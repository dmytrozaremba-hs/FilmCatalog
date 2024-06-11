using AutoMapper;
using AutoMapper.QueryableExtensions;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Application.Common.Models;
using FilmCatalog.Application.FilmLists.Helpers;
using FilmCatalog.Application.Films.Queries.GetAll;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.FilmLists.Queries.GetWatchedByUser;

public class GetWatchedByUserQuery : IRequest<PaginatedList<FilmBriefDto>>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public class GetWatchedByUserQueryHandler : IRequestHandler<GetWatchedByUserQuery, PaginatedList<FilmBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetWatchedByUserQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<PaginatedList<FilmBriefDto>> Handle(GetWatchedByUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetCurrentUserAsync() ?? throw new ForbiddenAccessException();

        //var user =
        //    await _context.Users
        //        .Where(x => x.Id == request.UserId)
        //        .SingleOrDefaultAsync(cancellationToken);

        //if (user == null)
        //{
        //    throw new NotFoundException(nameof(User), request.UserId);
        //}

        var filmList =
            await _context.FilmLists.Include(x => x.Films)
                .Where(x => x.Id == user.WatchedId)
                .SingleOrDefaultAsync(cancellationToken);

        if (filmList == null)
        {
            throw new NotFoundException("Watched list for User", user.Id);
        }

        var filmIds = filmList.Films.Select(x => x.Id).ToList();

        var query = _context.Films.AsNoTracking();

        var filtered = query;
        filtered = filtered.Where(x => filmIds.Contains(x.Id));

        var ordered = filtered.OrderBy(x => x.Title);

        var result = await ordered
            .ProjectTo<FilmBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        foreach(var item in result.Items)
        {
            item.IncludedInWatchedList = true;
            item.IncludedInWatchLaterList = await FilmListHelper.FilmIncludedInWatchLaterList(_context, user, item.Id, cancellationToken);
        }

        return result;
    }
}