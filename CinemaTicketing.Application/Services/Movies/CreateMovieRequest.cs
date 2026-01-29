namespace CinemaTicketing.Application.Services.Movies;

public class CreateMovieRequest
{


    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public int Rating { get; set; } 

    public int DurationMinutes { get; set; }

    public string Description { get; set; } = null!;
}