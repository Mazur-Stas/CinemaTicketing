using CinemaTicketing.Domain.Abstractions;
using CinemaTicketing.Domain.Abstractions.Repositories;
using CinemaTicketing.Domain.Models.Movies;
using CinemaTicketing.Domain.Requests;
using CinemaTicketing.Domain.Response;
using Microsoft.Extensions.Logging;

namespace CinemaTicketing.Application.Services.Movies;

public class MovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MovieService> _logger;

    public MovieService(IMovieRepository movieRepository, IUnitOfWork unitOfWork, ILogger<MovieService> logger)
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ResponseList<MovieResponse>> GetAll(MovieRequest request, CancellationToken ct = default)
    {
        var response = await _movieRepository.GetAll(request, ct);

        return response.ToResponseList(ToMovieResponse);
    }
    
        public async ValueTask<Movie?> GetById(int id, CancellationToken cancellationToken = default)
    {
        var response = await _movieRepository.GetById(id, cancellationToken);

        return response == null 
            ? null 
            : response;
    }

    public async Task<MovieResponse> Add(CreateMovieRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(request.Title) || string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("Invalid movie name");
        }

        try
        {
            var movie = new Movie
            {
                Title = request.Title,
                Rating = request.Rating,
                Genre = request.Genre,
                DurationMinutes = request.DurationMinutes,
                Description = request.Description
            };

            _movieRepository.Add(movie);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Added new movie {movie.Title}");

            return ToMovieResponse(movie);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            throw;
        }
    }

    public async Task<MovieResponse> UpdateMovie(int id, CreateMovieRequest request,
        CancellationToken cancellationToken = default)
    {
        var movie = await _movieRepository.GetById(id, cancellationToken);
        

           movie.Title = request.Title;
           movie.Rating = request.Rating;
           movie.Genre = request.Genre;
           movie.DurationMinutes = request.DurationMinutes;
           movie.Description = request.Description;
        

        _movieRepository.Update(movie);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"Updated {movie.Title}");
        return ToMovieResponse(movie);
    }

    public async Task DeleteMovie(int id, CancellationToken cancellationToken = default)
    {
        var movie = await _movieRepository.GetById(id, cancellationToken);
        
        _movieRepository.Delete(movie);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    private MovieResponse ToMovieResponse(Movie movie)
    {
        return new MovieResponse
        { 
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre,
            Rating = movie.Rating,
            DurationMinutes = movie.DurationMinutes,
            Description = movie.Description
        };
    } 
}

