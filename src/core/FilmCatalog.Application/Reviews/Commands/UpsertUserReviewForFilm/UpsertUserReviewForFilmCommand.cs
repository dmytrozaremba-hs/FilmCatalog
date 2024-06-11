using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Identity.Commands.Login;
using FilmCatalog.Application.Identity;
using MediatR;
using FilmCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FilmCatalog.Domain.Common;
using FilmCatalog.Domain.Events;

namespace FilmCatalog.Application.Reviews.Commands.UpsertUserReviewForFilm;

public class UpsertUserReviewForFilmCommand : IRequest<ReviewDto>
{
    public int FilmId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }
}

public class UpsertUserReviewForFilmCommandHandler : IRequestHandler<UpsertUserReviewForFilmCommand, ReviewDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public UpsertUserReviewForFilmCommandHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<ReviewDto> Handle(UpsertUserReviewForFilmCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetCurrentUserAsync() ?? throw new ForbiddenAccessException();

        var film =
            await _context.Films
                .Where(x => x.Id == request.FilmId)
                .SingleOrDefaultAsync(cancellationToken);

        if (film == null)
        {
            throw new NotFoundException(nameof(Film), request.FilmId);
        }

        var review =
            await _context.Reviews
                .Where(x => x.UserId == user.Id && x.FilmId == request.FilmId)
                .SingleOrDefaultAsync(cancellationToken);

        if (review == null)
        {
            review = new Review { UserId = user.Id, FilmId = film.Id, Rating = 5 };

            _context.Reviews.Add(review);
        }

        review.Rating = request.Rating;
        review.Comment = request.Comment;

        review.DomainEvents.Add(new ReviewUpsertedEvent(review));

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ReviewDto>(review);
    }
}
