using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Persons.Commands.Update
{
    public class UpdatePersonCommand : IRequest
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public bool IsDirector { get; set; }

        public bool IsProducer { get; set; }

        public bool IsActor { get; set; }

        public DateTime BirthDate { get; set; }
    }

    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePersonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Persons
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Person), request.Id);
            }

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.MiddleName = request.MiddleName;
            entity.IsDirector = request.IsDirector;
            entity.IsProducer = request.IsProducer;
            entity.IsActor = request.IsActor;
            entity.BirthDate = request.BirthDate;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to update", ex);
            }
        }
    }

}
