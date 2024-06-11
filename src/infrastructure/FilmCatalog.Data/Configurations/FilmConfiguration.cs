using FilmCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmCatalog.Data.Configurations;

public class FilmConfiguration : IEntityTypeConfiguration<Film>
{
    public void Configure(EntityTypeBuilder<Film> builder)
    {
        builder
            .HasMany(e => e.Directors)
            .WithMany(e => e.DirectedFilms)
            .UsingEntity("DirectorFilms");

        builder
            .HasMany(e => e.Producers)
            .WithMany(e => e.ProducedFilms)
            .UsingEntity("ProducerFilms");

        builder
            .HasMany(e => e.Actors)
            .WithMany(e => e.ActedInFilms)
            .UsingEntity("ActorFilms");

        builder
            .HasMany(e => e.Genres)
            .WithMany(e => e.Films);

        builder
            .HasMany(e => e.Tags)
            .WithMany(e => e.Films);

        builder
            .HasMany(e => e.Reviews)
            .WithOne(e => e.Film);
    }
}
