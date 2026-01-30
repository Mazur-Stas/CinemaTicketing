using CinemaTicketing.Application.Services.Tickets;
using CinemaTicketing.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketing.Api.Controllers;

[ApiController]
[Route("api/v1/tickets")]
public class TicketsController : ControllerBase
{
    private readonly TicketService _ticketService;

    public TicketsController(TicketService ticketService)
    {
        _ticketService = ticketService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] TicketRequest request,
        CancellationToken ct)
    {
        var result = await _ticketService.GetAll(request, ct);
        return Ok(result);
    }
    
    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetByUserId(
        int userId,
        CancellationToken ct)
    {
        var result = await _ticketService.GetByUserId(userId, ct);
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken ct)
    {
        var ticket = await _ticketService.GetById(id, ct);

        if (ticket == null)
            return NotFound();

        return Ok(ticket);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTicketRequest request,
        CancellationToken ct)
    {
        var result = await _ticketService.Add(request, ct);
        return Ok(result);
    }

    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken ct)
    {
        await _ticketService.DeleteTicket(id, ct);
        return NoContent();
    }
}