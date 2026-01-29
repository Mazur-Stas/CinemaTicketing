namespace CinemaTicketing.Application.Services.Movies;

public class MovieResponse
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public int Rating { get; set; } 

    public int DurationMinutes { get; set; }

    public string Description { get; set; } = null!;

}