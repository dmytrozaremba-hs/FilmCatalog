using AutoMapper;
using AutoMapper.QueryableExtensions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Tags.Queries.GetAll;

public class GetAllTagsQuery : IRequest<PaginatedList<TagDto>>
{
    public string Search { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, PaginatedList<TagDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TagDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Tags.AsNoTracking();

        var filtered = query;
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            filtered = filtered.Where(x => x.Name.ToLower().Contains(request.Search.ToLower()));
        }

        var ordered = filtered.OrderBy(x => x.Name);

        return await ordered
            .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}