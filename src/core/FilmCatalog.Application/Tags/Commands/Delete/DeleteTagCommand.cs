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

namespace FilmCatalog.Application.Tags.Commands.Delete
{
    public class DeleteTagCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTagCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            try
            {
                _context.Tags.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to delete", ex);
            }
        }
    }

}
