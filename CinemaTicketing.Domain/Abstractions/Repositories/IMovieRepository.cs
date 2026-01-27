using CinemaTicketing.Domain.Models.Movies;
using CinemaTicketing.Domain.Requests;
using CinemaTicketing.Domain.Response;

namespace CinemaTicketing.Domain.Abstractions.Repositories;

public interface IMovieRepository
{
    Task<ResponseList<Movie>> GetAll(MovieRequest request,CancellationToken ct);
    ValueTask<Movie?> GetById(int id, CancellationToken ct);
    void Add(Movie movie);
    void Update(Movie movie);
    void Delete(Movie movie);
}
