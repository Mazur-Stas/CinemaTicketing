using CinemaTicketing.Application.Services.Auth.Jwt;
using CinemaTicketing.Domain.Abstractions;
using CinemaTicketing.Domain.Abstractions.Repositories;
using CinemaTicketing.Domain.Enums;
using CinemaTicketing.Domain.Models.Users;
using CinemaTicketing.Shared.Hash;

namespace CinemaTicketing.Application.Services.Auth;

public class AuthService
{
    private readonly IJwtGenerator _generator;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IJwtGenerator generator, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _generator = generator;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email, cancellationToken);

        if (user == null)
        {
           throw new ArgumentException($"User with email {request.Email} not found");
        }

        if (!HasherUtil.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new ArgumentException($"User with email {request.Email} does not match password");
        }
        
        return new AuthResponse
        {
            Token = _generator.GenerateToken(request.Email, user.Name),
            UserId = user.Id
        };
    }

    public async Task<AuthResponse> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        // validation

        var existingUser = await _userRepository.GetByEmail(request.Email, cancellationToken);

        if (existingUser != null)
        {
            throw new ArgumentException($"User with email {request.Email} already exists");
        }
        
        var user = new User
        {
            Email = request.Email,
            PasswordHash = HasherUtil.HashPassword(request.Password),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            Role = UserRole.Customer
        };
        
        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new AuthResponse
        {
            Token = _generator.GenerateToken(request.Email, user.Name),
            UserId = user.Id
        };
    }
}