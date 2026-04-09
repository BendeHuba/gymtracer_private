using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymTracer.models;

public partial class Payment : ModelBase<Payment>
{
    public long Id { get; set; }

    [Required]
    public long IssuerId { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    [Required]
    public ulong TotalPrice { get; set; }

    [Required]
    [StringLength(20)]
    public string ReceiptNumber { get; set; } = null!;

    public virtual User Issuer { get; set; } = null!;

    public virtual ICollection<UserTicket> UserTickets { get; set; } = new List<UserTicket>();
}
