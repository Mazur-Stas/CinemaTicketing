using CinemaTicketing.Domain.Enums;

namespace CinemaTicketing.Domain.Requests;

public class TicketRequest
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;

    public int? UserId { get; set; }
    public TicketStatus? Status { get; set; }
}
