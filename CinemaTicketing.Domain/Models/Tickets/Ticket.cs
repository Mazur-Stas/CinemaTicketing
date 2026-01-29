using CinemaTicketing.Domain.Enums;
using CinemaTicketing.Domain.Models.Base;
using CinemaTicketing.Domain.Models.Movies;
using CinemaTicketing.Domain.Models.Users;

namespace CinemaTicketing.Domain.Models.Tickets;

public class Ticket : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } 
    public int? MovieId { get; set; }
    public Movie? Movie { get; set; }


    public DateTime PurchaseDate { get; set; }

    public TicketStatus Status { get; set; }
}
