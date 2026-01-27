using CinemaTicketing.Domain.Abstractions.Repositories;
using CinemaTicketing.Domain.Models.Tickets;
using CinemaTicketing.Domain.Requests;
using CinemaTicketing.Domain.Response;
using CinemaTicketing.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure.Repositories;

public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
{
    public TicketRepository(CinemaContext context) : base(context) { }

    public async Task<ResponseList<Ticket>> GetAll(TicketRequest request, CancellationToken ct)
    {
        IQueryable<Ticket> query = Set<Ticket>()
            .Include(t => t.User)
            .Include(t => t.Movie);

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(t => t.Status == request.Status.Value);
        }

        var count = await query.CountAsync(ct);

        var items = await query
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(ct);


        return new ResponseList<Ticket>
        {
            Items = items,
            TotalCount = count,
            Limit = request.Limit,
            Offset = request.Offset,
            Page = 0 // request.Offset / request.Limit
        };
    }

    public async Task<ResponseList<Ticket>> GetByUserId(int? userId, CancellationToken ct)
    {
        IQueryable<Ticket> query = Set<Ticket>()
            .Include(t => t.Movie);

        if (userId.HasValue)
        {
            query = query.Where(t => t.UserId == userId.Value);
        }

        var count = await query.CountAsync(ct);

        var items = await query
            .ToListAsync(ct);

        return new ResponseList<Ticket>
        {
            Items = items,
            TotalCount = count,
            Limit = count,
            Offset = 0,
            Page = 0
        };
    }

}
