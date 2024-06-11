using FilmCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmCatalog.Data.Configurations;

public class FilmListConfiguration : IEntityTypeConfiguration<FilmList>
{
    public void Configure(EntityTypeBuilder<FilmList> builder)
    {
        builder
            .HasMany(e => e.Films)
            .WithMany(e => e.FilmLists);
    }
}
