namespace CinemaTicketing.Application.Services.Auth;

public class RegisterUserRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Name { get; set; }
}