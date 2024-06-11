using FilmCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    DbSet<Genre> Genres { get; }

    DbSet<Tag> Tags { get; }

    DbSet<Person> Persons { get; }

    DbSet<Film> Films { get; }

    DbSet<FilmList> FilmLists { get; }

    DbSet<User> Users { get; }

    DbSet<Review> Reviews { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
