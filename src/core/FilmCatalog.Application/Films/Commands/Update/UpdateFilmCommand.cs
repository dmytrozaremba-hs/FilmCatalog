using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Persons;
using FilmCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Films.Commands.Update
{
    public class UpdateFilmCommand : IRequest
    {
        public int Id { get; set; }

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

    public class UpdateFilmCommandHandler : IRequestHandler<UpdateFilmCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFilmCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateFilmCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Films
                .Where(x => x.Id == request.Id)
                .Include(x => x.Genres)
                .Include(x => x.Tags)
                .Include(x => x.Directors)
                .Include(x => x.Producers)
                .Include(x => x.Actors)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Film), request.Id);
            }

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.ReleaseDate = new DateTime(int.Parse(request.Year), 1, 1);
            entity.PosterUrl = request.PosterUrl;
            entity.DurationInMinutes = request.DurationInMinutes;

            foreach (var genre in entity.Genres.Where(x => request.Genres.All(y => y != x.Id)).ToList())
            {
                // deleted
                entity.Genres.Remove(genre);
            }

            foreach (var genreId in request.Genres)
            {
                    var genre = await _context.Genres
                    .Where(x => x.Id == genreId)
                    .SingleOrDefaultAsync(cancellationToken);

                    if (genre == null)
                    {
                        throw new InvalidOperationException("Genre does not exist");
                    }
                    if (!entity.Genres.Any(x => x.Id == genre.Id))
                        entity.Genres.Add(genre);
            }


            foreach (var tag in entity.Tags.Where(x => request.Tags.All(y => y != x.Id)).ToList())
            {
                // deleted
                entity.Tags.Remove(tag);
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
                if (!entity.Tags.Any(x => x.Id == tag.Id))
                    entity.Tags.Add(tag);

            }


            foreach (var director in entity.Directors.Where(x => request.Directors.All(y => y != x.Id)).ToList())
            {
                // deleted
                entity.Directors.Remove(director);
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
                if (!entity.Directors.Any(x => x.Id == director.Id))
                    entity.Directors.Add(director);
            }


            foreach (var producer in entity.Producers.Where(x => request.Producers.All(y => y != x.Id)).ToList())
            {
                // deleted
                entity.Producers.Remove(producer);
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
                if (!entity.Producers.Any(x => x.Id == producer.Id))
                    entity.Producers.Add(producer);
            }


            foreach (var actor in entity.Actors.Where(x => request.Actors.All(y => y != x.Id)).ToList())
            {
                // deleted
                entity.Actors.Remove(actor);
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
                if (!entity.Actors.Any(x => x.Id == actor.Id))
                    entity.Actors.Add(actor);
            }

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
