using CinemaTicketing.Domain.Models.Base;
using CinemaTicketing.Domain.Models.Tickets;

namespace CinemaTicketing.Domain.Models.Movies;

public class Movie : BaseEntity
{
    public int Id { get; set; }

    public string Title { get; set; } 

    public string Genre { get; set; } 

    public int Rating { get; set; } 

    public int DurationMinutes { get; set; }

    public string Description { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
