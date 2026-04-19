using GymTracer.Auth;
using GymTracer.Context;
using GymTracer.DataValidator;
using GymTracer.Extensions;
using GymTracer.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTracer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GateController : ControllerBase
    {
        private readonly GymTracerDbContext dbContext;
        private readonly TokenHandler tokenHandler;

        public GateController(GymTracerDbContext dbContext, TokenHandler tokenHandler)
        {
            this.dbContext = dbContext;
            this.tokenHandler = tokenHandler;
        }
        [Authorize(Roles = nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        [HttpPost("{gate_id}/card/{card_code}/enter")]
        public IActionResult EnterThroughGate([FromRoute] Usage_Gates gate_id, [FromRoute] Guid card_code, [FromQuery] bool force = false)
        {
            return this.Run(() =>
            {
                var gateValidator = Validator.Create(gate_id);
                gateValidator.Validate(g => g, "kapu kódja", "gate_id")
                             .InEnum();

                if (!gateValidator.IsValid)
                    return BadRequest(new
                    {
                        errors = gateValidator.Errors
                    });

                var dbCard = dbContext.Cards.FirstOrDefault(c => c.Code == card_code &&
                                                                (!c.RevokedAt.HasValue || c.RevokedAt.Value > tokenHandler.Now()));

                if (dbCard is null)
                    return BadRequest("A kártya nem érvényes");

                if (!force)
                {
                    bool hasValidTicket = dbContext.UserTickets.Any(ut =>
                        ut.UserId == dbCard.UserId &&
                        !ut.Ticket.TrainingId.HasValue &&
                        ut.ExpirationDate > tokenHandler.Now() &&
                        ut.Payment.PaymentDate.HasValue &&
                        (ut.Ticket.Type == Ticket_Type.monthly ||
                        (ut.Ticket.MaxUsage.HasValue && ut.UsageAmount < ut.Ticket.MaxUsage.Value))
                    );
                    if (!hasValidTicket)
                        return BadRequest("A felhasználónak nincs érvényes jegye.");
                }

                dbContext.UsageLogs.Add(new UsageLog()
                {
                    CardId = dbCard.Id,
                    UseDate = tokenHandler.Now(),
                    Gate = gate_id
                });

                dbContext.SaveChanges();

                return Ok(new { message = "Sikeres kártyahasználat!"});
            });
        }
        [Authorize(Roles = nameof(User_Role.staff) + "," + nameof(User_Role.admin))]
        [HttpPost("{gate_id}/card/{card_code}/enter-main")]
        public IActionResult EnterMainGate([FromRoute] Usage_Gates gate_id, [FromRoute] Guid card_code, [FromQuery] bool force = false)
        {
            return this.Run(() =>
            {
                var gateValidator = Validator.Create(gate_id);
                gateValidator.Validate(g => g, "kapu kódja", "gate_id").InEnum();

                if (!gateValidator.IsValid)
                    return BadRequest(new { errors = gateValidator.Errors });

                var now = tokenHandler.Now();
                var todayStart = now.Date;
                var tomorrowStart = todayStart.AddDays(1);

                var dbCard = dbContext.Cards.FirstOrDefault(c => c.Code == card_code &&
                                                                (!c.RevokedAt.HasValue || c.RevokedAt.Value > now));

                if (dbCard is null)
                    return BadRequest("A kártya nem érvényes.");

                if (!force)
                {
                    bool alreadyEnteredToday = dbContext.UsageLogs.Any(log =>
                        log.Card.UserId == dbCard.UserId &&
                        log.UseDate >= todayStart &&
                        log.UseDate < tomorrowStart);

                    if (!alreadyEnteredToday)
                    {
                        var activeTickets = dbContext.UserTickets
                            .Include(ut => ut.Ticket)
                            .Where(ut => ut.UserId == dbCard.UserId &&
                                         !ut.Ticket.TrainingId.HasValue &&
                                         ut.ExpirationDate > now &&
                                         ut.Payment.PaymentDate.HasValue)
                            .ToList();

                        var ticketToUse = activeTickets
                            .Where(ut => ut.Ticket.Type == Ticket_Type.monthly)
                            .OrderBy(ut => ut.ExpirationDate)
                            .FirstOrDefault();

                        if (ticketToUse == null)
                        {
                            ticketToUse = activeTickets
                                .Where(ut => (ut.Ticket.Type == Ticket_Type.daily || ut.Ticket.Type == Ticket_Type.x_usage)
                                             && ut.Ticket.MaxUsage.HasValue
                                             && ut.UsageAmount < ut.Ticket.MaxUsage.Value)
                                .OrderBy(ut => ut.ExpirationDate)
                                .FirstOrDefault();

                            if (ticketToUse == null)
                                return BadRequest("A felhasználónak nincs érvényes bérlete.");

                            ticketToUse.UsageAmount += 1;
                        }
                    }
                }

                dbContext.UsageLogs.Add(new UsageLog()
                {
                    CardId = dbCard.Id,
                    UseDate = now,
                    Gate = gate_id
                });

                dbContext.SaveChanges();

                return Ok(new { message = "Sikeres főkapu belépés!" });
            });
        }
    }
}
