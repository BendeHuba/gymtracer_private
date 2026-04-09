using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymTracer.models;

public enum Usage_Gates
{
    main_entrance,
    main_exit,
    locker_room
}

public partial class UsageLog : ModelBase<UsageLog>
{
    public long Id { get; set; }

    [Required]
    public long CardId { get; set; }

    [Required]
    public DateTime UseDate { get; set; }

    [Required]
    public Usage_Gates Gate { get; set; }

    public virtual Card Card { get; set; } = null!;
}
