using CinemaTicketing.Domain.Models.Tickets;
using CinemaTicketing.Domain.Requests;
using CinemaTicketing.Domain.Response;

namespace CinemaTicketing.Domain.Abstractions.Repositories;

public interface ITicketRepository
{
    Task<ResponseList<Ticket>> GetAll(TicketRequest request,CancellationToken ct);
    Task<ResponseList<Ticket>> GetByUserId(int? userId, CancellationToken ct);
    ValueTask<Ticket?> GetById(int id, CancellationToken ct);
    void Add(Ticket ticket);
    void Delete(Ticket ticket);
}
