using FilmCatalog.Domain.Common;

namespace FilmCatalog.Domain.Entities;

public class Person : AuditableEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public bool IsDirector { get; set; }

    public bool IsProducer { get; set; }

    public bool IsActor { get; set; }

    public DateTime BirthDate { get; set; }

    public IList<Film> DirectedFilms { get; set; } = new List<Film>();

    public IList<Film> ProducedFilms { get; set; } = new List<Film>();

    public IList<Film> ActedInFilms { get; set; } = new List<Film>();
}
