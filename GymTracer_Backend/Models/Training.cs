using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymTracer.models;

public partial class Training : ModelBase<Training>
{
    public long Id { get; set; }

    [Required]
    public long TrainerId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Image { get; set; } = null!;

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public ulong MaxParticipant { get; set; }

    [Required]
    public bool Active { get; set; }

    public virtual User Trainer { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<TrainingUser> TrainingUsers { get; set; } = new List<TrainingUser>();
}
