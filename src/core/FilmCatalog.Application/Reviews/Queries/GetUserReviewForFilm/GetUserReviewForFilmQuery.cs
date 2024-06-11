using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Films.Queries.Get;
using FilmCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Reviews.Queries.GetUserReviewForFilm;

public class GetUserReviewForFilmQuery : IRequest<ReviewDto>
{
    public int FilmId { get; set; }
}

public class GetUserReviewForFilmQueryHandler : IRequestHandler<GetUserReviewForFilmQuery, ReviewDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetUserReviewForFilmQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<ReviewDto> Handle(GetUserReviewForFilmQuery request, CancellationToken cancellationToken)
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
            review = new Review { User = user, Film = film, Rating = 5 };
        }

        return _mapper.Map<ReviewDto>(review);
    }
}