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

namespace FilmCatalog.Application.Films.Commands.Delete
{
    public class DeleteFilmCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteFilmCommandHandler : IRequestHandler<DeleteFilmCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteFilmCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteFilmCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Films
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Film), request.Id);
            }

            try
            {
                _context.Films.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to delete", ex);
            }
        }
    }

}
