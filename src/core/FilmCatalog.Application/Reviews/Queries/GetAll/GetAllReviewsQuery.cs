using AutoMapper;
using AutoMapper.QueryableExtensions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Reviews.Queries.GetAll;

public class GetAllReviewsQuery : IRequest<PaginatedList<ReviewDto>>
{
    public int UserId { get; set; }

    public int FilmId { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, PaginatedList<ReviewDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllReviewsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ReviewDto>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Reviews.AsNoTracking();

        var filtered = query;
        if (request.UserId != 0)
        {
            filtered = filtered.Where(x => x.UserId == request.UserId);
        }

        if (request.FilmId != 0)
        {
            filtered = filtered.Where(x => x.FilmId == request.FilmId);
        }

        var ordered = filtered.OrderByDescending(x => x.CreatedAt).ThenByDescending(x => x.Id);

        return await ordered
            .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}