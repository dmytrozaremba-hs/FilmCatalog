using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmCatalog.Application.Genres.Queries.Get
{
    public class GetGenreQuery : IRequest<GenreDto>
    {
        public int Id { get; set; }
    }

    public class GetGenreQueryHandler : IRequestHandler<GetGenreQuery, GenreDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenreDto> Handle(GetGenreQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                return new GenreDto();
            }

            var entity = await _context.Genres
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Genre), request.Id);
            }

            return _mapper.Map<GenreDto>(entity);
        }
    }
}
