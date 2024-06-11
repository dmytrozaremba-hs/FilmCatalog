using FilmCatalog.Domain.Common;
using FilmCatalog.Domain.Entities;

namespace FilmCatalog.Domain.Events;

public class ReviewUpsertedEvent : DomainEvent
{
    public ReviewUpsertedEvent(Review review)
    {
        Review = review;
    }

    public Review Review { get; }
}
