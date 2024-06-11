using FilmCatalog.Domain.Common;
using FilmCatalog.Domain.Enums;

namespace FilmCatalog.Domain.Entities;

public class User : AuditableEntity
{
    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string HashedPassword { get; set; } = string.Empty;

    public Role Role { get; set; }

    public int WatchLaterId { get; set; }

    public FilmList WatchLater { get; set; }

    public int WatchedId { get; set; }

    public FilmList Watched { get; set; }
}
