using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymTracer.models;

public enum User_Role
{
    customer,
    trainer,
    staff,
    admin
}

public partial class User : ModelBase<User>
{
    public long Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(128)]
    public string Password { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    [Required]
    public User_Role Role { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }

    [Required]
    public bool Active { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();

    public virtual ICollection<Training> Training { get; set; } = new List<Training>();

    public virtual ICollection<TrainingUser> TrainingUsers { get; set; } = new List<TrainingUser>();

    public virtual ICollection<UserTicket> UserTickets { get; set; } = new List<UserTicket>();

    public void UpdateFrom(User model)
    {
        if (model.Name != null && !string.IsNullOrEmpty(model.Name.Trim()))
            Name = model.Name;
        if (model.Email != null)
            Email = model.Email;
        if (model.BirthDate != null)
            BirthDate = model.BirthDate;
    }
}
