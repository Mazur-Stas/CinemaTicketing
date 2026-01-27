namespace CinemaTicketing.Domain.Requests;

public class MovieRequest
{
    public string? Search { get; set; }
    public int Limit { get; set; } = 10;
    public int Offset { get; set; }
}