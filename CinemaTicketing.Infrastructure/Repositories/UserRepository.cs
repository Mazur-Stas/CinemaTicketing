using CinemaTicketing.Domain.Abstractions.Repositories;
using CinemaTicketing.Domain.Models.Users;
using CinemaTicketing.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(CinemaContext context) : base(context)
    {
    }

    public Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return Set<User>().FirstOrDefaultAsync(u => u.Email.Equals(email), cancellationToken);
    }

    public Task<bool> IsEmailExist(string email, CancellationToken cancellationToken)
    {
        return Set<User>().AnyAsync(u => u.Email.Equals(email.Trim()), cancellationToken);
    }
}