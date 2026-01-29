using CinemaTicketing.Application.Services.Auth;
using CinemaTicketing.Application.Services.Auth.Jwt;
using CinemaTicketing.Domain.Abstractions;
using CinemaTicketing.Infrastructure;
using CinemaTicketing.Infrastructure.Notifications;

namespace CinemaTicketing.Api;

using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



public static class DependencyInjection
{
    public static IServiceCollection AddCinemaApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        
        services.AddDbContext<CinemaContext>(options => options.UseSqlServer(configuration.GetConnectionString("Local")));
        services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<CinemaContext>());

        services.Scan(scan => scan
            .FromAssembliesOf(
                typeof(CinemaContext),
                typeof(AuthService)
            )
            .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository", StringComparison.Ordinal)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            
            .AddClasses(c => c.Where(t =>
                t.Name.EndsWith("Service", StringComparison.Ordinal) &&
                t != typeof(NotificationService)))
            .AsSelf()
            .WithScopedLifetime());

        services.AddTransient<IJwtGenerator, JwtGenerator>();
        services.AddTransient<INotificationService, NotificationService>();

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RoleClaimType = ClaimTypes.Role,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
                };
                
            });
        
        return services;
    }
}