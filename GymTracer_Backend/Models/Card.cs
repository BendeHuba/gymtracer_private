using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymTracer.models;

public partial class Card : ModelBase<Card>
{
    public long Id { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    [Required]
    public Guid Code { get; set; }

    public virtual ICollection<UsageLog> UsageLogs { get; set; } = new List<UsageLog>();

    public virtual User User { get; set; } = null!;
}
