using CinemaTicketing.Application.Services.Movies;
using CinemaTicketing.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketing.Api.Endpoints;

public static class MovieEndpoints
{
       public static IEndpointRouteBuilder UseMoviesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/movies");
        
        group.MapGet("/", async ([FromBody] MovieRequest request, CancellationToken cancellationToken, MovieService service) =>
        {
            var movies = await service.GetAll(request, cancellationToken);
            return Results.Ok(movies);
        })
        .RequireAuthorization();

        group.MapPost("/", async ([FromBody] CreateMovieRequest request, CancellationToken cancellationToken, MovieService service) =>
        {
            var movie = await service.Add(request, cancellationToken);
            return Results.Created($"api/v1/movies/{movie.Id}", movie);
        })
        .RequireAuthorization();

        group.MapGet("{id}", async ([FromRoute] int id, CancellationToken cancellationToken, MovieService service) =>
        {
            var movie = await service.GetById(id, cancellationToken);
            return Results.Ok(movie);
        })
        .RequireAuthorization();
        
        
        group.MapPut("{Id}", async ([FromRoute] int id, [FromBody] CreateMovieRequest request, CancellationToken cancellationToken, MovieService service) =>
        {
            var movie = await service.UpdateMovie(id, request, cancellationToken);
            return Results.Ok(movie);
        })
        .RequireAuthorization();
        
        group.MapDelete("{Id}", async ([FromRoute] int id, CancellationToken cancellationToken, MovieService service) =>
        {
            await service.DeleteMovie(id, cancellationToken);
            return Results.NoContent();
        })
        .RequireAuthorization();
        
        return app;
    }
}