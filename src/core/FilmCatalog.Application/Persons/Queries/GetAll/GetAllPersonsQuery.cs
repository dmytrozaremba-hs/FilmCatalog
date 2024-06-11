using AutoMapper;
using AutoMapper.QueryableExtensions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Persons.Queries.GetAll;

public class GetAllPersonsQuery : IRequest<PaginatedList<PersonBriefDto>>
{
    public string Search { get; set; } = string.Empty;

    public bool MustBeDirector { get; set; } = false;

    public bool MustBeProducer { get; set; } = false;

    public bool MustBeActor { get; set; } = false;

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, PaginatedList<PersonBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllPersonsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PersonBriefDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Persons.AsNoTracking();

        var filtered = query;

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var searchWords = request.Search.Split(" ").Select(x => x.Trim().ToLower()).Where(x => x != string.Empty).ToList();
            foreach (var searchWord in searchWords)
            {
                filtered =
                    filtered
                        .Where(
                            x =>
                                x.LastName.ToLower().Contains(searchWord) ||
                                x.FirstName.ToLower().Contains(searchWord) ||
                                x.MiddleName.ToLower().Contains(searchWord)
                        );
            }
        }

        if (request.MustBeDirector)
        {
            filtered = filtered.Where(x => x.DirectedFilms.Any());
        }

        if (request.MustBeProducer)
        {
            filtered = filtered.Where(x => x.ProducedFilms.Any());
        }

        if (request.MustBeActor)
        {
            filtered = filtered.Where(x => x.ActedInFilms.Any());
        }

        var ordered = filtered.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);

        return await ordered
            .ProjectTo<PersonBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}