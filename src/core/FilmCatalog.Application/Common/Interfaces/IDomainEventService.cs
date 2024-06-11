using FilmCatalog.Domain.Common;

namespace FilmCatalog.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
