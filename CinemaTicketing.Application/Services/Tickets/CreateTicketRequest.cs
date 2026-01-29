namespace CinemaTicketing.Application.Services.Tickets;

public class CreateTicketRequest
{
    public string Name { get; set; }
    public int UserId { get; set; }
    public int? MovieId { get; set; }
}