using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.FilmLists.Helpers;
using FilmCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Films.Queries.Get;

public class GetFilmQuery : IRequest<FilmDto>
{
    public int Id { get; set; }
}

public class GetFilmQueryHandler : IRequestHandler<GetFilmQuery, FilmDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetFilmQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<FilmDto> Handle(GetFilmQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == 0)
        {
            return new FilmDto();
        }

        var film =
            await _context.Films
                .Include(x => x.Genres)
                .Include(x => x.Tags)
                .Include(x => x.Directors)
                .Include(x => x.Producers)
                .Include(x => x.Actors)
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (film == null)
        {
            throw new NotFoundException(nameof(Film), request.Id);
        }

        var result = _mapper.Map<FilmDto>(film);

        var user = await _identityService.GetCurrentUserAsync();
        if (user != null)
        {
            result.IncludedInWatchedList = await FilmListHelper.FilmIncludedInWatchedList(_context, user, film, cancellationToken);
            result.IncludedInWatchLaterList = await FilmListHelper.FilmIncludedInWatchLaterList(_context, user, film, cancellationToken);
        }

        return result;
    }
}