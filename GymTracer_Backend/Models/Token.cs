using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GymTracer.models;

public partial class Token : ModelBase<Token>
{
    public long Id { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime RevokedAt { get; set; }

    [Required]
    [StringLength(128)]
    public string TokenString { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
