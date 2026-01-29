using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CinemaTicketing.Application.Services.Auth.Jwt;

public class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration _configuration;

    public JwtGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string email, string nickname)
    {
        
        var secretKey = _configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("SecretKey");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, nickname),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "User") 
        };

        
        var token = new JwtSecurityToken(
            issuer: "http://localhost",
            audience: "http://localhost",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}