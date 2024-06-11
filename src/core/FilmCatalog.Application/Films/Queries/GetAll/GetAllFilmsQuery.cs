using AutoMapper;
using AutoMapper.QueryableExtensions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Application.Common.Models;
using FilmCatalog.Application.FilmLists.Helpers;
using FilmCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Films.Queries.GetAll;

public class GetAllFilmsQuery : IRequest<PaginatedList<FilmBriefDto>>
{
    public string Search { get; set; } = string.Empty;

    public string SelectedGenres { get; set; } = string.Empty;

    public string SelectedTags { get; set; } = string.Empty;

    public int SelectedDirector { get; set; } = 0;

    public int SelectedProducer { get; set; } = 0;

    public int SelectedActor { get; set; } = 0;

    public int Sort { get; set; } = 5;

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public class GetAllFilmsQueryHandler : IRequestHandler<GetAllFilmsQuery, PaginatedList<FilmBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetAllFilmsQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<PaginatedList<FilmBriefDto>> Handle(GetAllFilmsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Films.AsNoTracking();

        var filtered = query;

        //if (!string.IsNullOrWhiteSpace(request.Search))
        //{
        //    filtered = filtered.Where(x => x.Title.Contains(request.Search));
        //}

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var searchWords = request.Search.Split(" ").Select(x => x.Trim().ToLower()).Where(x => x != string.Empty).ToList();
            foreach (var searchWord in searchWords)
            {
                filtered =
                    filtered
                        .Where(
                            x =>
                                x.Title.ToLower().Contains(searchWord)
                        );
            }
        }

        if (!string.IsNullOrWhiteSpace(request.SelectedGenres))
        {
            var selectedGenres =
                request.SelectedGenres.Split(',')
                    .Select(s => int.TryParse(s, out int v) ? v : (int?)null)
                    .Where(v => v.HasValue)
                    .ToList();
            if (selectedGenres.Any())
            {
                filtered = filtered.Where(x => x.Genres.Any(g => selectedGenres.Contains(g.Id)));
            }
        }

        if (!string.IsNullOrWhiteSpace(request.SelectedTags))
        {
            var selectedTags =
                request.SelectedTags.Split(',')
                    .Select(s => int.TryParse(s, out int v) ? v : (int?)null)
                    .Where(v => v.HasValue)
                    .ToList();
            if (selectedTags.Any())
            {
                filtered = filtered.Where(x => x.Tags.Any(g => selectedTags.Contains(g.Id)));
            }

        }

        if (request.SelectedDirector != 0)
        {
            filtered = filtered.Where(x => x.Directors.Any(p => p.Id == request.SelectedDirector));
        }

        if (request.SelectedProducer != 0)
        {
            filtered = filtered.Where(x => x.Producers.Any(p => p.Id == request.SelectedProducer));
        }

        if (request.SelectedActor != 0)
        {
            filtered = filtered.Where(x => x.Actors.Any(p => p.Id == request.SelectedActor));
        }

        var ordered = request.Sort switch
        {
            1 => filtered.OrderByDescending(x => x.AverageRating),
            2 => filtered.OrderBy(x => x.AverageRating),
            3 => filtered.OrderByDescending(x => x.ReleaseDate),
            4 => filtered.OrderBy(x => x.ReleaseDate),
            5 => filtered.OrderBy(x => x.Title),
            6 => filtered.OrderByDescending(x => x.Title),
            _ => filtered.OrderBy(x => x.Title) // default
        };


        var result = await ordered
            .ProjectTo<FilmBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        var user = await _identityService.GetCurrentUserAsync();
        if (user != null)
        {
            foreach (var item in result.Items)
            {
                item.IncludedInWatchedList = await FilmListHelper.FilmIncludedInWatchedList(_context, user, item.Id, cancellationToken);
                item.IncludedInWatchLaterList = await FilmListHelper.FilmIncludedInWatchLaterList(_context, user, item.Id, cancellationToken);
            }
        }

        return result;
    }
}