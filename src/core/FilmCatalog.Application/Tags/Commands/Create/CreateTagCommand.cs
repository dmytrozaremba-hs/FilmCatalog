using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Entities;
using MediatR;

namespace FilmCatalog.Application.Tags.Commands.Create
{
    public class CreateTagCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTagCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var entity = new Tag
            {
                Name = request.Name
            };

            try
            {
                _context.Tags.Add(entity);

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
