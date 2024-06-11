using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Entities;
using MediatR;

namespace FilmCatalog.Application.Persons.Commands.Create
{
    public class CreatePersonCommand : IRequest<int>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public bool IsDirector { get; set; }

        public bool IsProducer { get; set; }

        public bool IsActor { get; set; }

        public DateTime BirthDate { get; set; }
    }

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePersonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var entity = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                IsDirector = request.IsDirector,
                IsProducer = request.IsProducer,
                IsActor = request.IsActor,
                BirthDate = request.BirthDate,
            };

            try
            {
                _context.Persons.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to create", ex);
            }

            return entity.Id;
        }
    }
}
