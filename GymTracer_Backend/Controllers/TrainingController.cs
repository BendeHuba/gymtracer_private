using GymTracer.Auth;
using GymTracer.Context;
using GymTracer.DataValidator;
using GymTracer.Extensions;
using GymTracer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GymTracer.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly GymTracerDbContext dbContext;
        private readonly TokenHandler tokenHandler;

        public TrainingController(GymTracerDbContext dbContext, TokenHandler tokenHandler)
        {
            this.dbContext = dbContext;
            this.tokenHandler = tokenHandler;
        }

        [HttpGet]
        public IActionResult GetAllTrainings(
            [FromQuery] DateTime? start,
            [FromQuery] DateTime? end,
            [FromQuery] string? trainerName,
            [FromQuery] long? trainerId,
            [FromQuery] string? keyword)
        {
            return this.Run(() =>
            {
                var query = dbContext.Trainings
                    .Include(t => t.Trainer)
                    .Where(t => t.Active);

                if (!start.HasValue)
                    start = tokenHandler.Now().Date;

                if (!end.HasValue)
                    end = start.Value.Date.AddDays(1)
                                          .AddSeconds(-1);

                query = query.Where(t => 
                    t.StartTime >= start.Value && t.StartTime <= end.Value
                );

                if (trainerId.HasValue)
                    query = query.Where(t => t.TrainerId == trainerId.Value);

                if (!string.IsNullOrWhiteSpace(trainerName))
                    query = query.Where(t => t.Trainer.Name.Contains(trainerName));

                if (!string.IsNullOrWhiteSpace(keyword))
                    query = query.Where(t => t.Name.Contains(keyword) ||
                                             t.Description.Contains(keyword));

                var trainings = query
                    .OrderBy(t => t.StartTime)
                    .ToList();

                return Ok(trainings.Select(t => new
                {
                    t.Id,

                    t.Name,
                    t.Description,
                    t.Image,
                    t.StartTime,
                    t.EndTime,
                    t.MaxParticipant,

                    trainer = new
                    {
                        t.Trainer.Id,
                        t.Trainer.Name
                    }
                }));
            });
        }

        [Authorize(Roles = nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        [HttpGet("user/{id}")]
        public IActionResult GetTrainingsByTrainer(long id)
        {
            return this.Run(() =>
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                string? userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if(userId is null || userRole is null)
                    return BadRequest("Hibás token!");

                if(userId != id.ToString())
                {
                    if(userRole != nameof(User_Role.admin) && userRole != nameof(User_Role.staff))
                        return BadRequest("Csak saját nevedben kérheted le az edzéseket!");
                    User? selectedUser = dbContext.Users.FirstOrDefault(u => u.Id == id);
                    if(selectedUser is null)
                        return BadRequest("Nincs ilyen felhasználó!");
                }
                var trainings = dbContext.Trainings.Where(t => t.TrainerId == id && t.Active)
                                                   .OrderByDescending(t => t.EndTime);

                return Ok(trainings.Select(t => new
                {
                    t.Id,

                    t.Name,
                    t.Description,
                    t.Image,
                    t.StartTime,
                    t.EndTime,
                    t.MaxParticipant,

                    trainer = new
                    {
                        t.Trainer.Id,
                        t.Trainer.Name
                    }
                }));
            });
        }

        [Authorize]
        [HttpGet("{training_id}")]
        // Trainer (ha övé az edzés), staff és admin megkapja a jelentkezett usereket is
        public IActionResult GetTrainingById(long training_id)
        {
            return this.Run(() =>
            {
                string? userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                var trainingQuery = dbContext.Trainings
                    .Include(t => t.Trainer)
                    .Include(t => t.Tickets)
                    .AsQueryable();

                bool includeUsers =
                    userRole == nameof(User_Role.trainer) ||
                    userRole == nameof(User_Role.staff) ||
                    userRole == nameof(User_Role.admin);

                if (includeUsers)
                    trainingQuery = trainingQuery.Include(t => t.TrainingUsers)
                                                 .ThenInclude(tu => tu.User);

                Training? training = trainingQuery
                    .AsSplitQuery()
                    .FirstOrDefault(t => t.Id == training_id && t.Active);

                if (training is null)
                    return BadRequest("Nincs ilyen edzés!");

                bool isApplied = false;
                if (long.TryParse(userId, out long parsedUserId))
                {
                    isApplied = dbContext.TrainingUsers.Any(tu =>
                        tu.TrainingId == training_id &&
                        tu.UserId == parsedUserId);
                }

                bool returnUsers = includeUsers &&
                    (userRole != nameof(User_Role.trainer) || training.TrainerId.ToString() == userId);

                var trainer = new
                {
                    training.Trainer.Id,
                    training.Trainer.Name
                };

                var tickets = training.Tickets.Where(ticket => ticket.IsActive).Select(ticket => new
                {
                    ticket.Id,

                    ticket.Description,
                    ticket.IsStudent,
                    ticket.Type,
                    ticket.Price,
                    ticket.Tax_key,

                    ticket.MaxUsage,
                    ticket.IsActive
                });

                if(returnUsers)
                    return Ok(new
                    {
                        training.Id,

                        training.Name,

                        isApplied,
                        training.Description,
                        training.Image,
                        training.StartTime,
                        training.EndTime,
                        training.MaxParticipant,

                        trainer,
                        tickets,

                        users = training.TrainingUsers.Select(tu => new
                        {
                            tu.User.Id,
                            tu.User.Name,
                            tu.OnWaitinglist,
                            tu.Presence,
                            tu.ApplicationDate
                        })
                    });

                return Ok(new
                {
                    training.Id,

                    training.Name,

                    isApplied,
                    training.Description,
                    training.Image,
                    training.StartTime,
                    training.EndTime,
                    training.MaxParticipant,

                    trainer,
                    tickets,
                });
            });
        }

        [Authorize(Roles = nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        [HttpPost("user/{id}")]
        public IActionResult CreateTraining(long id, [FromBody] dynamic body)
        {
            return this.Run(() =>
            {
                Training training = Training.Deserialize(body);

                string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                string? userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if(userId is null || userRole is null)
                    return BadRequest("Hibás token!");

                if (userId != id.ToString())
                    if (userRole != nameof(User_Role.admin) && userRole != nameof(User_Role.staff))
                        return BadRequest("Csak saját nevedben készíthetsz edzést!");

                var ValidatorResult = ValidateTraining(training);
                if (!ValidatorResult.IsValid)
                    return BadRequest(new { ValidatorResult.Errors });

                if (dbContext.Trainings.Any(t => t.Active && t.StartTime < training.EndTime && training.StartTime < t.EndTime))
                    return BadRequest("Ebben az időintervallumban már van regisztrált edzés!");

                User? dbTrainer = dbContext.Users.FirstOrDefault(u => u.Id == id);

                if(dbTrainer is null)
                    return BadRequest("Nincs ilyen felhasználó!");

                Training dbTraining = new Training()
                {
                    Name = training.Name,
                    Description = training.Description,
                    Image = training.Image,
                    StartTime = training.StartTime,
                    EndTime = training.EndTime,
                    MaxParticipant = training.MaxParticipant,
                    TrainerId = dbTrainer.Id,
                    Active = true,
                    Tickets = new List<Ticket>()
                };

                var TicketValidatorResult = ValidateTickets(training.Tickets);
                if (!TicketValidatorResult.IsValid)
                    return BadRequest(new { TicketValidatorResult.Errors });

                // Training Ticketjeinek létrehozása (ha meg lettek adva)
                foreach (Ticket ticket in training.Tickets)
                {
                    dbTraining.Tickets.Add(new Ticket()
                    {
                        Description = ticket.Description,
                        IsStudent = ticket.IsStudent,
                        Price = ticket.Price,
                        Type = ticket.Type,
                        MaxUsage = 1,
                        Tax_key = 27,
                        IsActive = true
                    });
                }

                dbContext.Trainings.Add(dbTraining);
                dbContext.SaveChanges();

                return Ok(new
                {
                    message = "Az edzés sikeresen regisztrálva lett!",
                    training = new
                    {
                        dbTraining.Id,

                        dbTraining.Name,
                        dbTraining.Description,
                        dbTraining.Image,
                        dbTraining.StartTime,
                        dbTraining.EndTime,
                        dbTraining.MaxParticipant,

                        trainer = new
                        {
                            dbTrainer.Id,
                            dbTrainer.Name
                        },
                        tickets = dbTraining.Tickets.Where(t => t.IsActive).Select(ticket => new
                        {
                            ticket.Id,

                            ticket.Description,
                            ticket.IsStudent,
                            ticket.Type,
                            ticket.Price,
                            ticket.Tax_key,

                            ticket.MaxUsage,
                            ticket.IsActive
                        })
                    }
                });
            });
        }

        [Authorize(Roles = nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        [HttpPut("{training_id}")]
        public IActionResult UpdateTraining([FromRoute] long training_id, [FromBody] dynamic body)
        {
            return this.Run(() =>
            {
                Training training = Training.Deserialize(body);

                string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                string? userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (userId is null || userRole is null)
                    return BadRequest("Hibás token!");

                Training? dbTraining = dbContext.Trainings
                                    .Include(t => t.Trainer)
                                    .Include(t => t.Tickets)
                                    .FirstOrDefault(t => t.Id == training_id && t.Active);

                if (dbTraining is null)
                    return BadRequest("Nincs ilyen edzés!");

                if (userId != dbTraining.TrainerId.ToString())
                    if (userRole != nameof(User_Role.admin) && userRole != nameof(User_Role.staff))
                        return BadRequest("Csak a saját edzésedet módosíthatod!");

                if (training.TrainerId == 0)
                    training.TrainerId = dbTraining.TrainerId;

                if (dbTraining.TrainerId != training.TrainerId)
                    if (userRole != nameof(User_Role.admin) && userRole != nameof(User_Role.staff))
                        return BadRequest("Az edzést nem adhatja át másnak.");

                var ValidatorResult = ValidateTraining(training);
                if (!ValidatorResult.IsValid)
                    return BadRequest(new { ValidatorResult.Errors });

                if (dbContext.Trainings.Any(t => t.Active && t.Id != training_id && t.StartTime < training.EndTime && training.StartTime < t.EndTime))
                    return BadRequest("Ebben az időintervallumban már van regisztrált edzés!");

                var TicketValidatorResult = ValidateTickets(training.Tickets);
                if (!TicketValidatorResult.IsValid)
                    return BadRequest(new { TicketValidatorResult.Errors });

                var incomingTicketIds = training.Tickets.Where(t => t.Id != 0)
                                                        .Select(t => t.Id)
                                                        .ToList();

                var ticketsToRemove = dbTraining.Tickets.Where(db_t => db_t.IsActive && !incomingTicketIds.Contains(db_t.Id))
                                                        .ToList();

                foreach (Ticket toRemove in ticketsToRemove)
                {
                    toRemove.IsActive = false;
                    // TODO: megnézni hogy ki lett-e fizetve valakinek, ha igen, refundolni
                }

                foreach (Ticket incomingTicket in training.Tickets)
                {
                    if (incomingTicket.Id == 0)
                    {
                        dbTraining.Tickets.Add(new Ticket()
                        {
                            Description = incomingTicket.Description,
                            IsStudent = incomingTicket.IsStudent,
                            Price = incomingTicket.Price,
                            Type = incomingTicket.Type,
                            MaxUsage = 1,
                            Tax_key = 27,
                            IsActive = true
                        });
                    }
                    else
                    {
                        var existingDbTicket = dbTraining.Tickets.FirstOrDefault(db_t => db_t.Id == incomingTicket.Id && db_t.IsActive);
                        if (existingDbTicket is not null)
                        {
                            existingDbTicket.Description = incomingTicket.Description;
                            existingDbTicket.IsStudent = incomingTicket.IsStudent;
                            existingDbTicket.Price = incomingTicket.Price;
                            existingDbTicket.Type = incomingTicket.Type;
                        }
                    }
                }

                dbTraining.Name = training.Name;
                dbTraining.Description = training.Description;
                dbTraining.Image = training.Image;
                dbTraining.StartTime = training.StartTime;
                dbTraining.EndTime = training.EndTime;
                dbTraining.MaxParticipant = training.MaxParticipant;
                dbTraining.TrainerId = training.TrainerId;

                dbContext.SaveChanges();
                return Ok(new
                {
                    message = "Az edzés sikeresen módosítva lett!",
                    training = new
                    {
                        dbTraining.Id,

                        dbTraining.Name,
                        dbTraining.Description,
                        dbTraining.Image,
                        dbTraining.StartTime,
                        dbTraining.EndTime,
                        dbTraining.MaxParticipant,

                        trainer = new
                        {
                            dbTraining.Trainer.Id,
                            dbTraining.Trainer.Name
                        },
                        tickets = dbTraining.Tickets.Where(db_t => db_t.IsActive).Select(db_t => new
                        {
                            db_t.Id,
                            db_t.Description,
                            db_t.IsStudent,
                            db_t.Price,
                            db_t.Type,
                            db_t.MaxUsage,
                            db_t.IsActive
                        })
                    }
                });
            });
        }

        [Authorize(Roles = nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        [HttpDelete("{training_id}")]
        public IActionResult DeleteTraining([FromRoute] long training_id)
        {
            return this.Run(() =>
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                string? userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (userId is null || userRole is null)
                    return BadRequest("Hibás token!");

                Training? dbTraining = dbContext.Trainings.FirstOrDefault(t => t.Id == training_id && t.Active);

                if (dbTraining is null)
                    return BadRequest("Nincs ilyen edzés!");

                if (userId != dbTraining.TrainerId.ToString())
                {
                    if (userRole != nameof(User_Role.admin) && userRole != nameof(User_Role.staff))
                        return BadRequest("Csak saját nevében törölheti az edzést!");
                }

                dbTraining.Active = false;
                // TODO: jegyek inaktiválása, és megnézni hogy ki lett-e fizetve valakinek, ha igen, refundolni

                dbContext.SaveChanges();

                return Ok(new
                {
                    message = "Az edzés sikeresen törölve lett!"
                });
            });
        }

        [Authorize(Roles = nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        [HttpPatch("{training_id}/user/{id}/presence")]
        public IActionResult SetTrainingPresence([FromRoute] long training_id, [FromRoute] long id, [FromBody] dynamic body)
        {
            return this.Run(() =>
            {
                string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                string? userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (userId is null || userRole is null)
                    return BadRequest("Hibás token!");

                Training? dbTraining = 
                    dbContext.Trainings.Include(t => t.TrainingUsers)
                                       .FirstOrDefault(t => t.Id == training_id && t.Active);

                if (dbTraining is null)
                    return BadRequest("Nincs ilyen edzés!");

                bool? presence = body.presence;

                if(presence is null)
                    return BadRequest("A részvétel megadása kötelező!");

                if (userId != dbTraining.TrainerId.ToString())
                {
                    if (userRole != nameof(User_Role.admin) && userRole != nameof(User_Role.staff))
                        return BadRequest("Csak saját edzéséhez állíthatja az emberek jelenlétét!");
                }

                TrainingUser? dbTrainingUser = dbTraining.TrainingUsers.FirstOrDefault(t => t.UserId == id);
                if (dbTrainingUser is null) 
                    return BadRequest("Ez a felhasználó nincs regisztrálva erre az edzésre!");

                dbTrainingUser.Presence = presence.Value;
                dbContext.SaveChanges();

                return Ok(new{ 
                    Message = "A részvétel sikeresen módosítva lett!",
                    dbTrainingUser.TrainingId,
                    dbTrainingUser.UserId,
                    dbTrainingUser.Presence
                });
            });
        }

        [NonAction]
        public Validator<Training> ValidateTraining(Training training)
        {
            var trainingValidator = Validator.Create(training);

            trainingValidator.Validate(t => t.StartTime, "edzés kezdete")
                .NotDefault()
                .After(tokenHandler.Now())
                .Before(tokenHandler.Now().AddMonths(1));

            trainingValidator.Validate(t => t.EndTime, "edzés vége")
                .NotDefault()
                .After(training.StartTime)
                .Before(tokenHandler.Now().AddMonths(1));

            // Edzés hossza
            trainingValidator.Validate(t =>
              (t.EndTime - t.StartTime).TotalMinutes,
              "edzés időtartalma", "StartTime")
                .Between(5, 2 * 60);

            trainingValidator.Validate(t => t.Name, "edzés név")
                .NotNullOrEmpty();

            trainingValidator.Validate(t => t.Description, "edzés leírás")
                .NotNullOrEmpty();

            trainingValidator.Validate(t => t.Image, "edzés kép")
                .NotNullOrEmpty();
            // TODO: ellenőrizni valós kép-e

            trainingValidator.Validate(t => t.MaxParticipant, "résztvevő szám")
                .GreaterThan(0ul)
                .LessThan(100ul);

            return trainingValidator;
        }

        [NonAction]

        public Validator<ICollection<Ticket>> ValidateTickets(ICollection<Ticket> tickets)
        {
            var ticketsValidator = Validator.Create(tickets);
            ticketsValidator.Validate(t => t, "Ticket", "jegy")!
                .NotNull()!
                .ForEach(ticket =>
                {
                    ticket.ThenValidate(ticket => ticket.Description, "leírás")
                        .NotNullOrEmpty();

                    ticket.ThenValidate(ticket => ticket.Price, "ár")
                        .Min(0ul)
                        .Max(ulong.MaxValue);

                    ticket.ThenValidate(ticket => ticket.Type, "típus")
                        .InEnum();
                });

            return ticketsValidator;
        }
    }
}
