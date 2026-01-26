using CinemaTicketing.Domain.Models.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaTicketing.Infrastructure.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasMany(m => m.Tickets)
            .WithOne(t => t.Movie)
            .HasForeignKey(t => t.MovieId);

        builder.HasQueryFilter(m => !m.DeletedAt.HasValue);
    }
}