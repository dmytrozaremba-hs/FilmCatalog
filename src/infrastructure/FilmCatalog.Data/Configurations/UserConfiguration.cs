using FilmCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmCatalog.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(fl => fl.WatchLater)
                .WithOne().HasForeignKey<User>(u => u.WatchLaterId);

        builder.HasOne(fl => fl.Watched)
            .WithOne().HasForeignKey<User>(u => u.WatchedId);
    }
}
