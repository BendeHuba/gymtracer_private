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

                    var cardOfUser = DbContext.Set<Card>().Where(c => c.UserId == user!.Id);
                    List<long> cardsIds = [];
                    foreach (var c in cardOfUser)
                    {
                        cardsIds.Add(c.Id);
                    }

                    return StatusCode(200, new
                    {
                        user = new
                        {
                            name = user!.Name,
                            email = user.Email,
                            birthDate = user.BirthDate,
                            creationDate = user.CreationDate,
                            cards = cardsIds,
                        }
                    });
                }
                else
                {
                    throw new ApiException(401, "Unauthorized");
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

                    var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var loggedInUser = DbContext.Set<User>().FirstOrDefault(u => u.Id.ToString() == loggedInUserId);

                    if (userToModifyWith.Email != loggedInUser!.Email)
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
                        throw new ApiException(404, "User not found");
                    }
                }
                else
                {
                    throw new ApiException(401, "Unauthorized");
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
                        throw new ApiException(404, "User not found");
                    }
                }
                else
                {
                    throw new ApiException(401, "Unauthorized");
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

                    var cardsOfUser = DbContext.Set<Card>().Where(c => c.UserId == user!.Id && c.RevokedAt == null).ToList();

                    if (cardsOfUser.Count != 0)
                    {
                        return StatusCode(200, cardsOfUser.Select(c => new
                        {
                            c.Id,
                            c.Code
                        }));
                    }
                    else
                    {
                        throw new ApiException(404, "No card found for this user");
                    }
                }
                else
                {
                    throw new ApiException(401, "Unauthorized");
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
                        c.Code
                    }));
                    
                }
                else
                {
                    throw new ApiException(401, "Unauthorized");
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
                    throw new ApiException(401, "Unauthorized");
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
                    throw new ApiException(401, "Unauthorized");
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
                        throw new ApiException(400, "User not found");
                    }

                    var training = DbContext.Set<Training>().FirstOrDefault(u => u.Id == training_id && u.Active && u.EndTime >= tokenHandler.Now());

                    if (training == null)
                    {
                        throw new ApiException(400, "No training avaible with this id");
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
                        throw new ApiException(400, "User already applied to this training");
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
                                throw new ApiException(400, "Ticket creation unsuccessful");
                            }
                        }
                        else
                        {
                            throw new ApiException(400, "No objectresult returned");
                        }
                    }
                    else
                    {
                        throw new ApiException(400, "Incorrect ticket for training");
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
                    throw new ApiException(401, "Unauthorized");
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
                        throw new ApiException(404, "No application to be deleted");
                    }
                    if (userticket.Payment.PaymentDate != null)
                    {
                        throw new ApiException(400, "Ticket was payed! No refound can be provided!");
                    }

                    var trainingUser = DbContext.Set<TrainingUser>().FirstOrDefault(tu => tu.UserId == id && tu.TrainingId == training_id);
                    if (trainingUser == null)
                    {
                        throw new ApiException(404, "No application found");
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

                    return StatusCode(204, "Application successfully deleted");

                }
                else
                {
                    throw new ApiException(401, "Unauthorized");
                }

            });

        }

        [HttpGet("")]
        [Authorize(Roles = nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult GetUserByParameter([FromQuery] string? name, [FromQuery] string? email)
        {
            return this.Run(() =>
            {
                List<User> users = [];
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email))
                {
                    return StatusCode(400, "At least one parameter requierd");
                }
                else if (string.IsNullOrEmpty(name))
                {
                    users = DbContext.Set<User>().Where(u => u.Email.Contains(email)).ToList();
                }
                else if (string.IsNullOrEmpty(email))
                {
                    users = DbContext.Set<User>().Where(u => u.Name.Contains(name)).ToList();
                }
                else
                {
                    throw new ApiException(400, "Bad request");
                }

                if (users.Count == 0)
                {
                    throw new ApiException(400, "No user found");
                }

                return StatusCode(200, users.Select(u => new
                    {
                        name = u.Name,
                        email = u.Email,
                        birthDate = u.BirthDate,
                        creationDate = u.CreationDate,
                    }
                ));
            });
        }
        
        [HttpPut("{id}/role")]
        [Authorize(Roles = nameof(User_Role.admin))]
        public IActionResult ModifyRoleOfUser(int id, [FromBody] User_Role role)
        {
            return this.Run(() =>
            {
                var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var loggedInUser = DbContext.Set<User>().FirstOrDefault(u => u.Id.ToString() == loggedInUserId);

                var user = DbContext.Set<User>().FirstOrDefault(u => u.Id == id);

                if (user != null)
                {
                    user.Role = role;
                }
                else
                {
                    throw new ApiException(400, "No user found");
                }
                if (loggedInUser!.Id == user.Id)
                {
                    throw new ApiException(400, "You can't modify your own role");
                }

                DbContext.Update(user);
                DbContext.SaveChanges();

                return StatusCode(200, "User role has been changed!");
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
                throw new ApiException(404, "User not found");
            }
            if (id.ToString() == loggedInUserId || (loggedInUser!.Role == User_Role.staff || loggedInUser.Role == User_Role.admin))
            {
                return true;
            }
            return false;
        }
    }
}
