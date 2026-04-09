using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymTracer.models;

public partial class UserTicket : ModelBase<UserTicket>
{
    public long Id { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    public long TicketId { get; set; }

    [Required]
    public long PaymentId { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }

    [Required]
    public DateTime ExpirationDate { get; set; }

    [Required]
    public ulong UsageAmount { get; set; }

    public virtual Payment Payment { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
