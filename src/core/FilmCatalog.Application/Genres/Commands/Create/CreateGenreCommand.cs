using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Entities;
using MediatR;

namespace FilmCatalog.Application.Genres.Commands.Create
{
    public class CreateGenreCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateGenreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var entity = new Genre
            {
                Name = request.Name
            };

            try
            {
                _context.Genres.Add(entity);

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
