using GymTracer.Auth;
using GymTracer.Context;
using GymTracer.Exceptions;
using GymTracer.Extensions;
using GymTracer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GymTracer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly GymTracerDbContext DbContext;
        private readonly TokenHandler tokenHandler;

        public StatisticController(GymTracerDbContext dbContext, TokenHandler tokenHandler)
        {
            this.DbContext = dbContext;
            this.tokenHandler = tokenHandler;
        }

        [HttpGet("gym")]
        [Authorize(Roles = nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        public IActionResult GetAttendanceStatistics([FromQuery] uint daysBack, [FromQuery] uint weeksBack)
        {
            return this.Run(() =>
            {
                if (daysBack > 7)
                {
                    throw new ApiException(400, "Only 7 days back statistic allowed at max");
                }

                if (weeksBack > 4)
                {
                    throw new ApiException(400, "Only 4 weeks back statistic allowed at max");
                }

                List<UsageLog> dayByStatistics = DbContext.Set<UsageLog>().Where(ul => ul.UseDate < tokenHandler.Now() && ul.UseDate > tokenHandler.Now().AddDays(-daysBack)).ToList();
                var dayBackReturn = dayByStatistics.GroupBy(s => s.UseDate.Date).Select(g => new
                {
                    date = g.Key.ToString("yyyy.MM.dd"),
                    guestNumber = g.Count()
                });

                List<UsageLog> weekByStatistics = DbContext.Set<UsageLog>().Where(ul => ul.UseDate < tokenHandler.Now() && ul.UseDate > tokenHandler.Now().AddDays(-weeksBack * 7)).ToList();
                var weekBackReturn = weekByStatistics.GroupBy(s => GetStartOfWeek(s.UseDate, DayOfWeek.Monday)).Select(g => new
                {
                    startOfWeek = g.Key,
                    guestNumber = g.Count()
                });

                return StatusCode(200, new { dayBackReturn, weekBackReturn });
            });
        }

        [HttpGet("tickets")]
        [Authorize(Roles = nameof(User_Role.admin))]
        public IActionResult GetTicketsStatistics()
        {
            return this.Run(() =>
            {
                var groupedUserTickets = DbContext.Set<UserTicket>().GroupBy(ut => ut.TicketId).ToList();
                var tickets = DbContext.Set<Ticket>().ToList();
                var ticketStatistics = groupedUserTickets.Select(g => new
                {
                    soldAmount = g.Count(),
                    ticket = tickets.AsEnumerable().Where(t => t.Id.ToString() == g.Key.ToString()).Select(t => new
                    {
                        t.Type,
                        t.Description,
                        t.IsStudent,
                        t.Price,
                        t.Tax_key,
                        t.MaxUsage
                    }).Single()
                });

                return StatusCode(200, ticketStatistics);
            });
        }

        [HttpGet("card")]
        [Authorize(Roles = nameof(User_Role.admin))]
        public IActionResult GetCardUseageLogs()
        {
            return this.Run(() =>
            {
               var cardLogs = DbContext.Set<UsageLog>().Include(ul => ul.Card).Include(ul => ul.Card.User).OrderByDescending(ul => ul.UseDate).Select(ul => new
               {
                   ul.UseDate,
                   cardId = ul.Card.Id,
                   userId = ul.Card.User.Id,
                   ul.Card.User.Name,
                   ul.Card.User.Email
               });

                return StatusCode(200, cardLogs);
            });
        }

        [NonAction]
        public DateTime GetStartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.Date.AddDays(-1 * diff);
        }
    }
}
