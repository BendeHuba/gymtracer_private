using GymTracer.Auth;
using GymTracer.Context;
using GymTracer.Exceptions;
using GymTracer.Extensions;
using GymTracer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GymTracer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController : ControllerBase
    {
        private readonly GymTracerDbContext DbContext;
        private readonly TokenHandler tokenHandler;

        public UserController(GymTracerDbContext dbContext, TokenHandler tokenHandler) {
            this.DbContext = dbContext;
            this.tokenHandler = tokenHandler;
        }

        [HttpGet("{id}/profile")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) +  "," + nameof(User_Role.admin))]
        public IActionResult GetUserById(int id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    var user = DbContext.Set<User>().FirstOrDefault(u => u.Id == id);

                    var wentInToday = DbContext.Set<UsageLog>().Any(log => log.Card.UserId == id 
                                                                         && log.Gate == Usage_Gates.main_entrance
                                                                         && log.UseDate >= tokenHandler.Now().Date
                                                                         && log.UseDate < tokenHandler.Now().Date.AddDays(1));

                    return StatusCode(200, new
                    {       
                            id = id,
                            name = user!.Name,
                            email = user.Email,
                            birthDate = user.BirthDate,
                            creationDate = user.CreationDate,
                            role = user.Role,
                            wentInToday = wentInToday,
                    });
                }
                else
                {
                    throw new ApiException(401, "Nem engedélyezett");
                }
            });
        }

        [GeneratedRegex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        private static partial Regex EmailRegex();

        [HttpPut("{id}/profile")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult ModifyUserData([FromBody] dynamic body, int id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    User userToModifyWith = models.User.Deserialize(body);

                    var userToModify = DbContext.Set<User>().SingleOrDefault(g => g.Id == id);

                    if (!EmailRegex().Match(userToModifyWith.Email).Success)
                        return BadRequest(new { error = "Az email címnek validnak kell lennie" });

                    var dbUser = DbContext.Set<User>().FirstOrDefault(u => u.Id == id);

                    if (userToModifyWith.Email != dbUser!.Email)
                    {
                        var isUsedEmail = DbContext.Users.Any(u => u.Email == userToModifyWith.Email);
                        if (isUsedEmail)
                            return BadRequest(new { error = "Az email cím már használatban van" });
                    }

                    if (userToModify != null)
                    {
                        userToModify.UpdateFrom(userToModifyWith);
                        DbContext.Update(userToModify);
                        DbContext.SaveChanges();
                        return GetUserById(id);
                    }
                    else
                    {
                        throw new ApiException(404, "Nincs ilyen felhasználó");
                    }
                }
                else
                {
                    throw new ApiException(401, "Nem engedélyezett");
                }
            });   
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult DeactivateUser(int id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    var userToDeactivate = DbContext.Set<User>().SingleOrDefault(g => g.Id == id);

                    var cardsOfUser = DbContext.Set<Card>().Where(c => c.UserId == userToDeactivate!.Id && c.RevokedAt == null).ToList();

                    var tokensOfUser = DbContext.Set<Token>().Where(t => t.UserId == id && t.RevokedAt > tokenHandler.Now()).ToList();

                    if (userToDeactivate != null && userToDeactivate.Active != false)
                    {
                        using var transaction = DbContext.Database.BeginTransaction();
                        userToDeactivate.Active = false;
                        DbContext.Update(userToDeactivate);
                        DbContext.SaveChanges();

                        if (cardsOfUser.Count != 0)
                        {
                            foreach (var c in cardsOfUser)
                            {
                                c.RevokedAt = tokenHandler.Now();
                                DbContext.Update(c);
                                DbContext.SaveChanges();
                            }
                        }

                        if (tokensOfUser.Count != 0)
                        {
                            foreach (var t in tokensOfUser)
                            {
                                t.RevokedAt = tokenHandler.Now();
                                DbContext.Update(t);
                                DbContext.SaveChanges();
                            }
                        }
                        transaction.Commit();

                        return StatusCode(204);
                    }
                    else
                    {
                        throw new ApiException(404, "Nincs ilyen felhasználó");
                    }
                }
                else
                {
                    throw new ApiException(401, "Nem engedélyezett");
                }

            });
            
        }

        [HttpGet("{id}/card")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult GetUsersCard(int id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    var user = DbContext.Set<User>().FirstOrDefault(u => u.Id == id);

                    var cardsOfUser = DbContext.Set<Card>().Include(c => c.UsageLogs).Where(c => c.UserId == user!.Id && c.RevokedAt == null).ToList();

                    if (cardsOfUser.Count != 0)
                    {
                        return StatusCode(200, cardsOfUser.Select(c => new
                        {
                            c.Id,
                            c.Code,
                            c.CreatedAt,
                        }));
                    }
                    else
                    {
                        throw new ApiException(404, "Ennek a felhasználónak aktív nincsenek kártyái");
                    }
                }
                else
                {
                    throw new ApiException(401, "Nem engedélyezett");
                }

            });

        }

        [HttpPost("{id}/card")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult PostNewCard(int id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    Card newCard = new Card();
                    newCard.UserId = id;
                    newCard.CreatedAt = tokenHandler.Now();
                    newCard.Code = Guid.NewGuid();


                    DbContext.Update(newCard);
                    DbContext.SaveChanges();

                    var card = DbContext.Set<Card>().Where(c => c.Code == newCard.Code);
                    return StatusCode(200, card.Select(c => new
                    {
                        c.Id,
                        c.Code,
                        c.CreatedAt
                    }));
                    
                }
                else
                {
                    throw new ApiException(401, "Nem engedélyezett");
                }

            });

        }

        [HttpDelete("{id}/card/{card_id}")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult DeleteCard(int id, int card_id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    var cardToDeactivate = DbContext.Set<Card>().FirstOrDefault(c => c.Id == card_id && c.RevokedAt == null);

                    if (cardToDeactivate != null)
                    {
                        cardToDeactivate.RevokedAt = tokenHandler.Now();

                        DbContext.Update(cardToDeactivate);
                        DbContext.SaveChanges();

                        return StatusCode(204);
                    }
                    else
                    {
                        throw new ApiException(404, "No card found");
                    }
                }
                else
                {
                    throw new ApiException(401, "Nem engedélyezett");
                }

            });

        }

        [HttpGet("{id}/training")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult GetTrainingsOfUser(int id, [FromQuery] bool arePreviousNeeded)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    var trainingsOfUser = DbContext.Set<TrainingUser>().Include(tu => tu.Training).Include(tu =>tu.Training.Trainer).Where(tu => tu.UserId == id);
                    if (arePreviousNeeded)
                    {
                        return StatusCode(200, trainingsOfUser.Select(tu => new
                        {
                            tu.Training.Name,
                            tu.Training.Image,
                            tu.Training.Description,
                            tu.Training.StartTime,
                            tu.Training.EndTime,
                            tu.Training.MaxParticipant,
                            tu.Training.Active,
                            TrainerName = tu.Training.Trainer.Name,
                            TrainerEmail = tu.Training.Trainer.Email,
                            tu.ApplicationDate,
                            tu.OnWaitinglist,
                            tu.Presence,
                        }));
                    }
                    else
                    {
                        return StatusCode(200, trainingsOfUser.Where(tu => tu.Training.EndTime > tokenHandler.Now()).Select(tu => new
                        {
                            tu.Training.Name,
                            tu.Training.Image,
                            tu.Training.Description,
                            tu.Training.StartTime,
                            tu.Training.EndTime,
                            tu.Training.MaxParticipant,
                            tu.Training.Active,
                            TrainerName = tu.Training.Trainer.Name,
                            TrainerEmail = tu.Training.Trainer.Email,
                            tu.ApplicationDate,
                            tu.OnWaitinglist,
                            tu.Presence,
                        }));
                    }
                }
                else
                {
                    throw new ApiException(401, "Nem engedélyezett");
                }

            });

        }

        [HttpPost("{id}/training/{training_id}/ticket/{ticket_id}")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult ApplyUserToTraining(int id, int training_id, int ticket_id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    var user = DbContext.Set<User>().FirstOrDefault(u => u.Id == id);
                    if (user == null)
                    {
                        throw new ApiException(400, "Nincs ilyen nfelhasználó");
                    }

                    var training = DbContext.Set<Training>().Include(t=> t.Trainer)
                                                            .FirstOrDefault(u => u.Id == training_id && u.Active && u.EndTime >= tokenHandler.Now());

                    if (training == null)
                    {
                        throw new ApiException(400, "Nincs ilyen edzés");
                    }
                    if (training.Trainer.Id == id)
                    {
                        throw new ApiException(400, "Saját edzésre nem lehet jelentkezni");
                    }

                    var userNumOnTraining = DbContext.Set<TrainingUser>().Where(tu => tu.TrainingId == training.Id && tu.OnWaitinglist == false).Count();

                    bool onWaitingList = (ulong)userNumOnTraining >= training.MaxParticipant;

                    TrainingUser newTrainingUser = new TrainingUser()
                    {
                        TrainingId = training.Id,
                        UserId = user!.Id,
                        ApplicationDate = tokenHandler.Now(),
                        OnWaitinglist = onWaitingList,
                        Presence = false,
                    };

                    var trainingUser = DbContext.Set<TrainingUser>().FirstOrDefault(tu => tu.TrainingId == training_id && tu.UserId == id);
                    if (trainingUser != null)
                    {
                        throw new ApiException(400, "A felhasználó már jelentkezett erre az edzésre");
                    }

                    if (DbContext.Set<Ticket>().Where(t => t.Id == ticket_id && t.TrainingId == training_id).SingleOrDefault() != null)
                    {
                        var TicketController = new TicketController(DbContext,tokenHandler);
                        var retunedData = TicketController.PostTicketAndPayment(id, ticket_id, false, true, User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                        if (retunedData is ObjectResult returnedObjectResult)
                        {
                            if (returnedObjectResult.StatusCode == 201)
                            {
                                //TODO: email küldése
                                DbContext.Set<TrainingUser>().Add(newTrainingUser);
                                DbContext.SaveChanges();
                            }
                            else
                            {
                                throw new ApiException(400, "Jegy sikeresen létrehozva");
                            }
                        }
                        else
                        {
                            throw new ApiException(500, "Internal server error");
                        }
                    }
                    else
                    {
                        throw new ApiException(400, "Nem megfelelő jegy az edzéshez");
                    }

                    return StatusCode(201, new
                    {
                        newTrainingUser.TrainingId,
                        newTrainingUser.UserId,
                        newTrainingUser.ApplicationDate,
                        newTrainingUser.OnWaitinglist,
                        newTrainingUser.Presence
                    });
                }
                else
                {
                    throw new ApiException(401, "Nem engedélyezett");
                }

            });

        }

        [HttpDelete("{id}/training/{training_id}")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult RemoveUserFromTraining(int id, long training_id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    var user = DbContext.Set<User>().FirstOrDefault(u => u.Id == id);
                    var training = DbContext.Set<Ticket>().Where(u => u.TrainingId == training_id).ToList();

                    UserTicket? userticket = null;
                    foreach (var ticket in training)
                    {
                        var userTicketSearch = DbContext.Set<UserTicket>().Include(ut => ut.Payment).FirstOrDefault(ut => ut.TicketId == ticket.Id && ut.UserId == id);
                        if (userTicketSearch != null)
                        {
                            userticket = userTicketSearch;
                        }
                    }
                    if (userticket == null)
                    {
                        throw new ApiException(404, "Nincs ilyen jelentkezés");
                    }
                    if (userticket.Payment.PaymentDate != null)
                    {
                        throw new ApiException(400, "A jegy már ki van kizetve! Sikertelen visszafizetés!");
                    }

                    var trainingUser = DbContext.Set<TrainingUser>().FirstOrDefault(tu => tu.UserId == id && tu.TrainingId == training_id);
                    if (trainingUser == null)
                    {
                        throw new ApiException(404, "Nincs ilyen jelentkezés");
                    }
                    
                    using var transaction = DbContext.Database.BeginTransaction();
                    DbContext.Set<Payment>().Remove(userticket.Payment);
                    DbContext.Set<UserTicket>().Remove(userticket);
                    DbContext.Set<TrainingUser>().Remove(trainingUser);
                    DbContext.SaveChanges();
                    
                    TrainingUser? userNextInQueueForTraining = DbContext.Set<TrainingUser>().Where(tu => tu.TrainingId == training_id && tu.OnWaitinglist == true).OrderBy(tu => tu.ApplicationDate).SingleOrDefault();
                    if (userNextInQueueForTraining != null)
                    {
                        userNextInQueueForTraining.OnWaitinglist = false;
                        DbContext.Update(userNextInQueueForTraining);
                        DbContext.SaveChanges();
                    }
                    transaction.Commit();

                    return StatusCode(204, new {message= "A jelentkezés sikeresen törölve" });

                }
                else
                {
                    throw new ApiException(401, "Nem engedélyezett");
                }

            });

        }

        [HttpGet("")]
        [Authorize(Roles = nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult GetUserByParameter([FromQuery] string? name, [FromQuery] string? email, [FromQuery] string? guid)
        {
            return this.Run(() =>
            {
                IQueryable<User> usersQuery = DbContext.Users.AsQueryable();

                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email) && string.IsNullOrEmpty(guid))
                    return StatusCode(400, new { message = "Legalább egy paraméter szükséges" });

                if (!string.IsNullOrEmpty(name))
                    usersQuery = usersQuery.Where(u => u.Name.Contains(name));
                if (!string.IsNullOrEmpty(email))
                    usersQuery = usersQuery.Where(u => u.Email.Contains(email));
                if (!string.IsNullOrEmpty(guid))
                    usersQuery = usersQuery.Where(u => u.Cards.Any(c => c.Code.ToString() == guid));

                return StatusCode(200, usersQuery.Select(u => new
                    {
                        id = u.Id,
                        name = u.Name,
                        email = u.Email,
                        birthDate = u.BirthDate,
                        creationDate = u.CreationDate,
                        role = u.Role,
                    }
                ));
            });
        }
        
        public record ModifyRoleOfUserDto(User_Role role);
        [HttpPut("{id}/role")]
        [Authorize(Roles = nameof(User_Role.admin))]
        public IActionResult ModifyRoleOfUser(int id, [FromBody] ModifyRoleOfUserDto roleDto)
        {
            return this.Run(() =>
            {
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var loggedInUser = DbContext.Set<User>().FirstOrDefault(u => u.Id.ToString() == loggedInUserId);

                var user = DbContext.Set<User>().FirstOrDefault(u => u.Id == id);

                if (user != null && Enum.IsDefined(roleDto.role))
                {
                    user.Role = roleDto.role;
                }
                else
                {
                    throw new ApiException(400, "Nincs ilyen felhasználó");
                }
                if (loggedInUser!.Id == user.Id)
                {
                    throw new ApiException(400, "Nem módosíthatod a saját szerepkörödet");
                }

                DbContext.Update(user);
                DbContext.SaveChanges();

                return StatusCode(200, new { message = "A felhasználó szerepköre megváltozott"});
            });

        }
        [NonAction]
        public bool IsAuthorized(int id)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedInUser = DbContext.Set<User>().FirstOrDefault(u => u.Id.ToString() == loggedInUserId);

            var user = DbContext.Set<User>().FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new ApiException(404, "Nincs ilyen felhasználó");
            }
            if (id.ToString() == loggedInUserId || (loggedInUser!.Role == User_Role.staff || loggedInUser.Role == User_Role.admin))
            {
                return true;
            }
            return false;
        }
    }
}
