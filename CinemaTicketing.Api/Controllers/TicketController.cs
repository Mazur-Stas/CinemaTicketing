using CinemaTicketing.Application.Services.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketing.Api.Controllers;

[ApiController]
[Route("api/v1/tickets")]
public class TicketController : ControllerBase
{
    private readonly TicketService _ticketService;
    
    public TicketController(TicketService ticketService)
    {
        _ticketService = ticketService;
    }
    
    
}