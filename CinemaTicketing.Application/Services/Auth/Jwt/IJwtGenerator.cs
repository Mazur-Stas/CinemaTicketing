namespace CinemaTicketing.Application.Services.Auth.Jwt;

public interface IJwtGenerator
{
    string GenerateToken(string email, string nickname);
}