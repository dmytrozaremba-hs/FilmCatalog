namespace FilmCatalog.Domain.Common;

public abstract class AuditableEntity: Entity
{
    public DateTime CreatedAt { get; set; }

    public DateTime? LastModifiedAt { get; set; }

}
