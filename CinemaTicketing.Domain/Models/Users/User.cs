using CinemaTicketing.Domain.Enums;
using CinemaTicketing.Domain.Models.Base;
using CinemaTicketing.Domain.Models.Tickets;

namespace CinemaTicketing.Domain.Models.Users;

public class User : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public UserRole Role { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
