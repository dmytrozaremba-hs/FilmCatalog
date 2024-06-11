using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Common.Models;
using FilmCatalog.Domain.Entities;
using FilmCatalog.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FilmCatalog.Application.Reviews.EventHandlers;

public class ReviewUpsertedEventHandler : INotificationHandler<DomainEventNotification<ReviewUpsertedEvent>>
{
    private readonly IApplicationDbContext _context;

    public ReviewUpsertedEventHandler(ILogger<ReviewUpsertedEventHandler> logger, IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DomainEventNotification<ReviewUpsertedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        var film =
            await _context.Films
                .Include(film => film.Reviews)
            .Where(x => x.Id == domainEvent.Review.FilmId)
            .SingleOrDefaultAsync(cancellationToken);

        if (film != null)
        {
            var numberOfVotes = film.Reviews.Count;
            var averageRating = 0.0;
            if (numberOfVotes > 0)
            {
                foreach (var review in film.Reviews)
                {
                    averageRating += (double)review.Rating / numberOfVotes;
                }
            }
            film.NumberOfVotes = numberOfVotes;
            film.AverageRating = averageRating;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
