using System.Security.Claims;
using GymTracer.Auth;
using GymTracer.Context;
using GymTracer.Exceptions;
using GymTracer.Extensions;
using GymTracer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTracer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly GymTracerDbContext DbContext;
        private readonly TokenHandler tokenHandler;

        public TicketController(GymTracerDbContext dbContext, TokenHandler tokenHandler)
        {
            this.DbContext = dbContext;
            this.tokenHandler = tokenHandler;
        }

        [HttpGet]
        public IActionResult GetAllTickets()
        {
            return this.Run(() =>
            {
                var tickets = DbContext.Set<Ticket>().Include(t => t.Training);

                var ticketsToBeReturned = tickets.Select(t => new
                {
                    t.Id,
                    t.Type,
                    t.Description,
                    t.IsStudent,
                    t.Price,
                    t.MaxUsage,
                    trainingId = t.Training == null ? null : t.TrainingId!,
                    trainerName = t.Training == null ? null : t.Training.Name!
                }).ToList();

                return StatusCode(200, ticketsToBeReturned);
            });
        }

        [HttpGet("user/{id}")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult GetTicketsOfAUser(int id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    var userTickets = DbContext.Set<UserTicket>().Where(u => u.UserId == id).Include(u => u.Ticket).Include(u => u.Ticket.Training);

                    if (userTickets != null)
                    {
                        return StatusCode(200, userTickets.Select(ut => new
                        {
                            ut.Ticket.Type,
                            ut.Ticket.Description,
                            ut.Ticket.IsStudent,
                            ut.ExpirationDate,
                            usagesLeft = ut.UsageAmount,
                            trainingId = ut.Ticket.Training == null ? null : ut.Ticket.TrainingId!,
                            trainerName = ut.Ticket.Training == null ? null : ut.Ticket.Training.Name!
                        }));
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

        [HttpGet("user/{id}/unpaid")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult GetUnpaidTIcketsOfAUser(int id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    var unpaidUserTickets = DbContext.Set<UserTicket>().Where(u => u.UserId == id).Include(u => u.Ticket).Include(u => u.Payment).Where(u => u.Payment.PaymentDate == null && u.Payment.DueDate > tokenHandler.Now());

                    if (unpaidUserTickets != null)
                    {
                        return StatusCode(200, unpaidUserTickets.Select(ut => new
                        {
                            paymentId = ut.PaymentId,
                            ut.Ticket.Type,
                            ut.Ticket.Description,
                            ut.Ticket.IsStudent,
                            ut.ExpirationDate,
                            price = ut.Payment.TotalPrice,
                            usagesLeft = ut.UsageAmount
                        }));
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

        [HttpPost("{ticket_id}/user/{id}/{is_paid}")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult PostTicketAndPayment(int id, int ticket_id, bool is_paid, bool calledFromOtherController = false, string issuerId = "")
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id, calledFromOtherController))
                {
                    using var transaction = DbContext.Database.BeginTransaction();
                    string loggedInUserId;
                    try
                    {
                        loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                    }
                    catch
                    {
                        loggedInUserId = issuerId;
                    }
                    if (loggedInUserId == "")
                    {
                        throw new ApiException(400, "Issuer can not be identified");
                    }


                    var user = DbContext.Set<User>().FirstOrDefault(u => u.Id == id);

                    var ticket = DbContext.Set<Ticket>().FirstOrDefault(t => t.Id == ticket_id);

                    var maxId = DbContext.Set<Payment>().Max(p => p.Id);
                    var nextRecipeNum = int.Parse(DbContext.Set<Payment>().Where(p => p.Id == maxId).Single().ReceiptNumber.Split("-")[1]) + 1;

                    if (ticket == null)
                    {
                        throw new ApiException(400, "No ticket found");
                    }

                    Payment newPayment = new Payment();

                    if (is_paid)
                    {
                        
                        newPayment = new Payment()
                        {
                            IssuerId = int.Parse(loggedInUserId!),
                            DueDate = tokenHandler.Now(),
                            PaymentDate = tokenHandler.Now(),
                            TotalPrice = ticket.Price,
                            ReceiptNumber = "REC-" + nextRecipeNum.ToString()
                        };
                    }
                    else
                    {
                        newPayment = new Payment()
                        {
                            IssuerId = int.Parse(loggedInUserId!),
                            DueDate = tokenHandler.Now().AddMonths(2),
                            PaymentDate = null,
                            TotalPrice = ticket.Price,
                            ReceiptNumber = "REC-" + nextRecipeNum.ToString()
                        };
                    }
                    
                    DbContext.Set<Payment>().Add(newPayment);
                    DbContext.SaveChanges();

                    var payment = DbContext.Set<Payment>().Where(p => p.ReceiptNumber == "REC-" +  nextRecipeNum.ToString()).Single();

                    DateTime expirationDate = tokenHandler.Now();

                    switch (ticket.Type)
                    {
                        case Ticket_Type.training:
                            expirationDate = expirationDate.AddYears(1);
                            break;
                        case Ticket_Type.daily:
                            expirationDate = expirationDate.AddDays(1);
                            break;
                        case Ticket_Type.monthly:
                            expirationDate = expirationDate.AddMonths(1);
                            break;
                        case Ticket_Type.x_usage:
                            expirationDate = expirationDate.AddYears(1);
                            break;

                    }

                    UserTicket newUserTicket = new UserTicket()
                    {
                        UserId = user!.Id,
                        TicketId = ticket_id,
                        PaymentId = payment.Id,
                        CreationDate = tokenHandler.Now(),
                        ExpirationDate = expirationDate,
                        UsageAmount = 0,
                    };

                    DbContext.Set<UserTicket>().Add(newUserTicket);
                    DbContext.SaveChanges();

                    transaction.Commit();

                    return StatusCode(201, new
                    {
                        id = payment.Id,
                        issuerId = payment.IssuerId,
                        dueDate = payment.DueDate,
                        paymentDate = payment.PaymentDate,
                        totalPrice = payment.TotalPrice,
                        receiptNumber = payment.ReceiptNumber,
                    });
                }
                else
                {
                    throw new ApiException(401, "Unauthorized");
                }
            });
        }

        [HttpPatch("user/{id}/pay/{payment_id}")]
        [Authorize(Roles = nameof(User_Role.customer) + "," + nameof(User_Role.trainer) + "," + nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult PatchPayment(int id, int payment_id)
        {
            return this.Run(() =>
            {
                if (IsAuthorized(id))
                {
                    var paymentToBePayed = DbContext.Set<UserTicket>().Where(ut => ut.UserId == id && ut.PaymentId == payment_id).Include(ut => ut.Payment).Single();

                    if (paymentToBePayed == null)
                    {
                        throw new ApiException(400, "No payment found");
                    }

                    if (paymentToBePayed.Payment.PaymentDate != null)
                    {
                        throw new ApiException(400, "Ticket already payed");
                    }

                    paymentToBePayed.Payment.PaymentDate = tokenHandler.Now();
                    DbContext.SaveChanges();

                    return StatusCode(201, new
                    {
                        id = paymentToBePayed.Payment.Id,
                        issuerId = paymentToBePayed.Payment.IssuerId,
                        dueDate = paymentToBePayed.Payment.DueDate,
                        paymentDate = paymentToBePayed.Payment.PaymentDate,
                        totalPrice = paymentToBePayed.Payment.TotalPrice,
                        receiptNumber = paymentToBePayed.Payment.ReceiptNumber,
                    });
                }
                else
                {
                    throw new ApiException(401, "Unauthorized");
                }
            });
        }




        [NonAction]
        public bool IsAuthorized(int id, bool skipLoggedInUserCheck = false)
        {
            if (skipLoggedInUserCheck)
            {
                return true;
            }
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
