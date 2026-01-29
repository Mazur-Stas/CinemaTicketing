namespace CinemaTicketing.Application.Services.Tickets;

public class TicketResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public int? MovieId { get; set; }
}