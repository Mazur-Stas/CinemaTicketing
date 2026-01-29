using CinemaTicketing.Application.Services.Movies;
using CinemaTicketing.Domain.Abstractions;
using CinemaTicketing.Domain.Abstractions.Repositories;
using CinemaTicketing.Domain.Models.Tickets;
using CinemaTicketing.Domain.Requests;
using CinemaTicketing.Domain.Response;
using Microsoft.Extensions.Logging;

namespace CinemaTicketing.Application.Services.Tickets;

public class TicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TicketService> _logger;
    
    public TicketService(ITicketRepository ticketRepository, IUnitOfWork unitOfWork, ILogger<TicketService> logger)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<ResponseList<TicketResponse>> GetAll(TicketRequest request, CancellationToken ct = default)
    {
        var response = await _ticketRepository.GetAll(request, ct);

        return response.ToResponseList(ToTicketResponse);
    }
    
    public async Task<ResponseList<TicketResponse>> GetByUserId(int userId, CancellationToken ct)
    {
        var response = await _ticketRepository.GetByUserId(userId, ct);

        return response.ToResponseList(ToTicketResponse);
    }
    
        
    public async ValueTask<Ticket?> GetById(int id, CancellationToken cancellationToken = default)
    {
        var response = await _ticketRepository.GetById(id, cancellationToken);

        return response == null 
            ? null 
            : response;
    }

    public async Task<TicketResponse> Add(CreateTicketRequest request, CancellationToken cancellationToken = default)
    {


        try
        {
            var ticket = new Ticket
            {
                Name = request.Name,
                UserId =  request.UserId,
                MovieId = request.MovieId,
            };

            _ticketRepository.Add(ticket);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Added new ticket {ticket.Name}");

            return ToTicketResponse(ticket);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            throw;
        }
    }
    public async Task DeleteTicket(int id, CancellationToken cancellationToken = default)
    {
        var ticket = await _ticketRepository.GetById(id, cancellationToken);
        
        _ticketRepository.Delete(ticket);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    private TicketResponse ToTicketResponse(Ticket ticket)
    {
        return new TicketResponse
        { 
            Id = ticket.Id,
            Name = ticket.Name,
            UserId = ticket.UserId,
            MovieId = ticket.MovieId,
        };
    } 

}