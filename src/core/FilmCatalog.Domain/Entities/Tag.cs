using FilmCatalog.Domain.Common;

namespace FilmCatalog.Domain.Entities;

public class Tag : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public IList<Film> Films { get; set; } = new List<Film>();
}
