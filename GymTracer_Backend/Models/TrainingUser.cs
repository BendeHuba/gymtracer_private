using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymTracer.models;

public partial class TrainingUser : ModelBase<TrainingUser>
{
    public long Id { get; set; }

    [Required]
    public long TrainingId { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    public DateTime ApplicationDate { get; set; }

    [Required]
    public bool OnWaitinglist { get; set; }

    [Required]
    public bool Presence { get; set; }

    public virtual Training Training { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
