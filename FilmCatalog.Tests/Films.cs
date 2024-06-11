using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Application.Films.Queries.Get;
using FilmCatalog.Application.Films.Queries.GetAll;
using FilmCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FilmCatalog.Tests
{
    // GetFilmQueryHandlerTests

    public class GetFilmQueryHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _dbContextMock;
        private readonly IMapper _mapper;

        public GetFilmQueryHandlerTests()
        {
            _dbContextMock = new Mock<IApplicationDbContext>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }
        [Fact]
        public async Task Handle_ValidId_ReturnsFilmDto()
        {
            // Arrange
            int filmId = 1;
            var film = new Film
            {
                Id = filmId,
                Title = "Film Title",
                Genres = new List<Genre>(),
                Directors = new List<Person>(),
                Producers = new List<Person>(),
                Actors = new List<Person>()
            };

            var films = new List<Film>
            {
                film
            };

            var filmDbSetMock = new Mock<DbSet<Film>>();
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.Provider).Returns(films.AsQueryable().Provider);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.Expression).Returns(films.AsQueryable().Expression);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.ElementType).Returns(films.AsQueryable().ElementType);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.GetEnumerator()).Returns(films.AsQueryable().GetEnumerator());

            _dbContextMock.Setup(d => d.Films).Returns(filmDbSetMock.Object);

            var query = new GetFilmQuery { Id = filmId };
            var queryHandler = new GetFilmQueryHandler(_dbContextMock.Object, _mapper, null);

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(filmId, result.Id);
            Assert.Equal("Film Title", result.Title);
        }

        [Fact]
        public async Task Handle_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            int filmId = 1;

            var films = new List<Film>();

            var filmDbSetMock = new Mock<DbSet<Film>>();
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.Provider).Returns(films.AsQueryable().Provider);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.Expression).Returns(films.AsQueryable().Expression);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.ElementType).Returns(films.AsQueryable().ElementType);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.GetEnumerator()).Returns(films.AsQueryable().GetEnumerator());

            _dbContextMock.Setup(d => d.Films).Returns(filmDbSetMock.Object);

            var query = new GetFilmQuery { Id = filmId };
            var queryHandler = new GetFilmQueryHandler(_dbContextMock.Object, _mapper, null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => queryHandler.Handle(query, CancellationToken.None));
        }
    }

    // GetAllFilmsQueryHandlerTests

    public class GetAllFilmsQueryHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _dbContextMock;
        private readonly IMapper _mapper;

        public GetAllFilmsQueryHandlerTests()
        {
            _dbContextMock = new Mock<IApplicationDbContext>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_NoFilters_ReturnsPaginatedListOfFilmBriefDto()
        {
            // Arrange
            var films = new List<Film>
        {
            new Film { Id = 1, Title = "Film 1" },
            new Film { Id = 2, Title = "Film 2" },
            new Film { Id = 3, Title = "Film 3" }
        };

            var filmDbSetMock = new Mock<DbSet<Film>>();
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.Provider).Returns(films.AsQueryable().Provider);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.Expression).Returns(films.AsQueryable().Expression);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.ElementType).Returns(films.AsQueryable().ElementType);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.GetEnumerator()).Returns(films.AsQueryable().GetEnumerator());

            _dbContextMock.Setup(d => d.Films).Returns(filmDbSetMock.Object);

            var query = new GetAllFilmsQuery();
            var queryHandler = new GetAllFilmsQueryHandler(_dbContextMock.Object, _mapper, null);

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.TotalCount);
            Assert.Equal(1, result.PageNumber);
            Assert.Equal("Film 1", result.Items[0].Title);
            Assert.Equal("Film 2", result.Items[1].Title);
            Assert.Equal("Film 3", result.Items[2].Title);
        }

        [Fact]
        public async Task Handle_WithFilters_ReturnsFilteredPaginatedListOfFilmBriefDto()
        {
            // Arrange
            var films = new List<Film>
        {
            new Film { Id = 1, Title = "Film 1", Genres = new List<Genre> { new Genre { Id = 1, Name = "Genre 1" } } },
            new Film { Id = 2, Title = "Film 2", Genres = new List<Genre> { new Genre { Id = 2, Name = "Genre 2" } } },
            new Film { Id = 3, Title = "Film 3", Genres = new List<Genre> { new Genre { Id = 1, Name = "Genre 1" } } }
        };

            var filmDbSetMock = new Mock<DbSet<Film>>();
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.Provider).Returns(films.AsQueryable().Provider);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.Expression).Returns(films.AsQueryable().Expression);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.ElementType).Returns(films.AsQueryable().ElementType);
            filmDbSetMock.As<IQueryable<Film>>().Setup(f => f.GetEnumerator()).Returns(films.AsQueryable().GetEnumerator());

            _dbContextMock.Setup(d => d.Films).Returns(filmDbSetMock.Object);

            var query = new GetAllFilmsQuery { SelectedGenres = "1" };
            var queryHandler = new GetAllFilmsQueryHandler(_dbContextMock.Object, _mapper, null);

            // Act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal(1, result.PageNumber);
            Assert.Equal("Film 1", result.Items[0].Title);
            Assert.Equal("Film 3", result.Items[1].Title);
        }
    }

}