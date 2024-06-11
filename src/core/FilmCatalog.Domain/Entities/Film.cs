using FilmCatalog.Domain.Common;

namespace FilmCatalog.Domain.Entities;

public class Film : AuditableEntity
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string PosterUrl { get; set; } = string.Empty;

    public IList<Genre> Genres { get; set; } = new List<Genre>();

    public IList<Tag> Tags { get; set; } = new List<Tag>();

    public IList<Person> Directors { get; set; } = new List<Person>();

    public IList<Person> Producers { get; set; } = new List<Person>();

    public IList<Person> Actors { get; set; } = new List<Person>();

    public DateTime ReleaseDate { get; set; }

    public int DurationInMinutes { get; set; }

    public double AverageRating { get; set; }

    public int NumberOfVotes { get; set; }

    public IList<FilmList> FilmLists { get; set; } = new List<FilmList>();

    public IList<Review> Reviews { get; set; } = new List<Review>();
}