using FilmCatalog.Domain.Common;

namespace FilmCatalog.Domain.Entities;

public class Review : AuditableEntity, IHasDomainEvent
{
    public int UserId { get; set; }

    public User User { get; set; }

    public int FilmId { get; set; }

    public Film Film { get; set; }

    public string Comment { get; set; } = string.Empty;

    public int Rating { get; set; }

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}