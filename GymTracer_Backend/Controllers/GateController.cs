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
                        ut.ExpirationDate > tokenHandler.Now()
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

                return Ok("Sikeres kártyahasználat!");
            });
        }
    }
}
