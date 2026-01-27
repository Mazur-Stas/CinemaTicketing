using CinemaTicketing.Domain.Abstractions.Repositories;
using CinemaTicketing.Domain.Models.Movies;
using CinemaTicketing.Domain.Requests;
using CinemaTicketing.Domain.Response;
using CinemaTicketing.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure.Repositories;

public class MovieRepository 
    : RepositoryBase<Movie>, IMovieRepository
{
    public MovieRepository(CinemaContext context) : base(context) { }

    public async Task<ResponseList<Movie>> GetAll(MovieRequest request, CancellationToken ct)
    {
        var query = Set<Movie>();
        
        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(m =>
                m.Title.Contains(request.Search) ||
                m.Description.Contains(request.Search));
        }
        var count = await query.CountAsync(ct);

        var items = await query
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(ct);
        
        return new ResponseList<Movie>
        {
            Items = items,
            TotalCount = count,
            Limit = request.Limit,
            Offset = request.Offset,
            Page = 0//request.Offset / request.Limit
        };
    }
}
