using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Tags.Queries.Get
{
    public class GetTagQuery : IRequest<TagDto>
    {
        public int Id { get; set; }
    }

    public class GetTagQueryHandler : IRequestHandler<GetTagQuery, TagDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTagQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TagDto> Handle(GetTagQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                return new TagDto();
            }

            var entity = await _context.Tags
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            return _mapper.Map<TagDto>(entity);
        }
    }
}
