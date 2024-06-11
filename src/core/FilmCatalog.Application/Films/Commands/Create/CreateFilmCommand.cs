using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Persons;
using FilmCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Films.Commands.Create
{
    public class CreateFilmCommand : IRequest<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Year { get; set; }

        public string PosterUrl { get; set; }

        public IList<int> Genres { get; set; } = new List<int>();

        public IList<int> Tags { get; set; } = new List<int>();

        public IList<int> Directors { get; set; } = new List<int>();

        public IList<int> Producers { get; set; } = new List<int>();

        public IList<int> Actors { get; set; } = new List<int>();

        public int DurationInMinutes { get; set; }
    }

    public class CreateFilmCommandHandler : IRequestHandler<CreateFilmCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateFilmCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFilmCommand request, CancellationToken cancellationToken)
        {
            var entity = new Film
            {
                Title = request.Title,
                Description = request.Description,
                ReleaseDate = new DateTime(int.Parse(request.Year), 1, 1),
                PosterUrl = request.PosterUrl,
                DurationInMinutes = request.DurationInMinutes,
            };

            foreach (var genreId in request.Genres)
            {
                var genre = await _context.Genres
                    .Where(x => x.Id == genreId)
                    .SingleOrDefaultAsync(cancellationToken);

                if (genre == null)
                {
                    throw new InvalidOperationException("Genre does not exist");
                }
                entity.Genres.Add(genre);
            }

            foreach (var tagId in request.Tags)
            {
                var tag = await _context.Tags
                    .Where(x => x.Id == tagId)
                    .SingleOrDefaultAsync(cancellationToken);

                if (tag == null)
                {
                    throw new InvalidOperationException("Tag does not exist");
                }
                entity.Tags.Add(tag);
            }

            foreach (var directorId in request.Directors)
            {
                var director = await _context.Persons
                    .Where(x => x.Id == directorId)
                    .SingleOrDefaultAsync(cancellationToken);

                if (director == null)
                {
                    throw new InvalidOperationException("Director does not exist");
                }
                entity.Directors.Add(director);
            }

            foreach (var producerId in request.Producers)
            {
                var producer = await _context.Persons
                    .Where(x => x.Id == producerId)
                    .SingleOrDefaultAsync(cancellationToken);

                if (producer == null)
                {
                    throw new InvalidOperationException("Producer does not exist");
                }
                entity.Producers.Add(producer);
            }

            foreach (var actorId in request.Actors)
            {
                var actor = await _context.Persons
                    .Where(x => x.Id == actorId)
                    .SingleOrDefaultAsync(cancellationToken);

                if (actor == null)
                {
                    throw new InvalidOperationException("Actor does not exist");
                }
                entity.Actors.Add(actor);
            }

            try
            {
                _context.Films.Add(entity);

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
