using CinemaTicketing.Domain.Abstractions;
using CinemaTicketing.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure;

public class CinemaContext : DbContext, IUnitOfWork
{
    public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}