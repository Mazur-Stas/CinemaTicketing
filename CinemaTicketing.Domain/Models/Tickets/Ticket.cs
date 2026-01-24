using CinemaTicketing.Domain.Enums;
using CinemaTicketing.Domain.Models.Base;
using CinemaTicketing.Domain.Models.Movies;
using CinemaTicketing.Domain.Models.Users;

namespace CinemaTicketing.Domain.Models.Tickets;

public class Ticket : BaseEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public DateTime PurchaseDate { get; set; }

    public TicketStatus Status { get; set; }
}
