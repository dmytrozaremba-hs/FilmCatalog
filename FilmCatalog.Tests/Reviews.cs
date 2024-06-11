using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Reviews;
using FilmCatalog.Application.Reviews.Commands.UpsertUserReviewForFilm;
using FilmCatalog.Application.Reviews.Queries.GetAll;
using FilmCatalog.Application.Reviews.Queries.GetUserReviewForFilm;
using FilmCatalog.Domain.Entities;
using Moq;
using Xunit;

namespace FilmCatalog.Tests
{
    public class UpsertUserReviewForFilmCommandHandlerTests
    {
        private Mock<IApplicationDbContext> _dbContextMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IIdentityService> _identityServiceMock;
        private UpsertUserReviewForFilmCommandHandler _handler;

        public UpsertUserReviewForFilmCommandHandlerTests()
        {
            _dbContextMock = new Mock<IApplicationDbContext>();
            _mapperMock = new Mock<IMapper>();
            _identityServiceMock = new Mock<IIdentityService>();
            _handler = new UpsertUserReviewForFilmCommandHandler(
                _dbContextMock.Object,
                _mapperMock.Object,
                _identityServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_WithExistingFilmAndReview_ReturnsUpdatedReviewDto()
        {
            // Arrange
            var command = new UpsertUserReviewForFilmCommand
            {
                FilmId = 1,
                Rating = 8,
                Comment = "Good movie"
            };

            var user = new User { Id = 1 };
            var film = new Film { Id = 1 };
            var existingReview = new Review { UserId = 1, FilmId = 1, Rating = 3, Comment = "Average movie" };

            _identityServiceMock.Setup(s => s.GetCurrentUserAsync()).ReturnsAsync(user);

            _mapperMock.Setup(m => m.Map<ReviewDto>(It.IsAny<Review>()))
                .Returns((Review review) => new ReviewDto { Rating = review.Rating, Comment = review.Comment });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Rating, result.Rating);
            Assert.Equal(command.Comment, result.Comment);
        }

        [Fact]
        public async Task Handle_WithExistingFilmAndNoReview_ReturnsCreatedReviewDto()
        {
            // Arrange
            var command = new UpsertUserReviewForFilmCommand
            {
                FilmId = 1,
                Rating = 4,
                Comment = "Good movie"
            };

            var user = new User { Id = 1 };
            var film = new Film { Id = 1 };

            _identityServiceMock.Setup(s => s.GetCurrentUserAsync()).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<ReviewDto>(It.IsAny<Review>()))
                .Returns((Review review) => new ReviewDto { Rating = review.Rating, Comment = review.Comment });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Rating, result.Rating);
            Assert.Equal(command.Comment, result.Comment);
        }

        [Fact]
        public async Task Handle_WithNonExistingFilm_ThrowsNotFoundException()
        {
            // Arrange
            var command = new UpsertUserReviewForFilmCommand
            {
                FilmId = 1,
                Rating = 4,
                Comment = "Good movie"
            };

            var user = new User { Id = 1 };

            _identityServiceMock.Setup(s => s.GetCurrentUserAsync()).ReturnsAsync(user);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

    public class GetAllReviewsQueryHandlerTests
    {
        private Mock<IApplicationDbContext> _dbContextMock;
        private Mock<IMapper> _mapperMock;
        private GetAllReviewsQueryHandler _handler;

        public GetAllReviewsQueryHandlerTests()
        {
            _dbContextMock = new Mock<IApplicationDbContext>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetAllReviewsQueryHandler(_dbContextMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WithUserId_ReturnsPaginatedListReviewDto()
        {
            // Arrange
            var query = new GetAllReviewsQuery
            {
                UserId = 1,
                PageNumber = 1,
                PageSize = 10
            };

            var reviews = new List<Review>
        {
            new Review { UserId = 1, FilmId = 1, Rating = 4, Comment = "Good movie" },
            new Review { UserId = 1, FilmId = 2, Rating = 3, Comment = "Average movie" }
        };

            _dbContextMock.Setup(d => d.Reviews)
                .Returns(MockHelper.CreateMockDbSet(reviews).Object);

            _mapperMock.Setup(m => m.ConfigurationProvider)
                .Returns(new MapperConfiguration(cfg =>
                    cfg.CreateMap<Review, ReviewDto>()));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reviews.Count, result.TotalCount);
            Assert.Equal(query.PageNumber, result.PageNumber);
            Assert.Equal(reviews.Count, result.Items.Count());
        }

        [Fact]
        public async Task Handle_WithFilmId_ReturnsPaginatedListReviewDto()
        {
            // Arrange
            var query = new GetAllReviewsQuery
            {
                FilmId = 1,
                PageNumber = 1,
                PageSize = 10
            };

            var reviews = new List<Review>
        {
            new Review { UserId = 1, FilmId = 1, Rating = 4, Comment = "Good movie" },
            new Review { UserId = 2, FilmId = 1, Rating = 3, Comment = "Average movie" }
        };

            _dbContextMock.Setup(d => d.Reviews)
                .Returns(MockHelper.CreateMockDbSet(reviews).Object);

            _mapperMock.Setup(m => m.ConfigurationProvider)
                .Returns(new MapperConfiguration(cfg =>
                    cfg.CreateMap<Review, ReviewDto>()));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reviews.Count, result.TotalCount);
            Assert.Equal(query.PageNumber, result.PageNumber);
            Assert.Equal(reviews.Count, result.Items.Count());
        }
    }

    public class GetUserReviewForFilmQueryHandlerTests
    {
        private Mock<IApplicationDbContext> _dbContextMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IIdentityService> _identityServiceMock;
        private GetUserReviewForFilmQueryHandler _handler;

        public GetUserReviewForFilmQueryHandlerTests()
        {
            _dbContextMock = new Mock<IApplicationDbContext>();
            _mapperMock = new Mock<IMapper>();
            _identityServiceMock = new Mock<IIdentityService>();
            _handler = new GetUserReviewForFilmQueryHandler(
                _dbContextMock.Object,
                _mapperMock.Object,
                _identityServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_WithValidFilmId_ReturnsReviewDto()
        {
            // Arrange
            var query = new GetUserReviewForFilmQuery
            {
                FilmId = 1
            };

            var user = new User { Id = 1 };
            var film = new Film { Id = 1 };
            var review = new Review { User = user, Film = film, Rating = 8, Comment = "Good movie" };

            _identityServiceMock.Setup(s => s.GetCurrentUserAsync())
                .ReturnsAsync(user);

            _dbContextMock.Setup(d => d.Films)
                .Returns(MockHelper.CreateMockDbSet(new List<Film> { film }).Object);

            _dbContextMock.Setup(d => d.Reviews)
                .Returns(MockHelper.CreateMockDbSet(new List<Review> { review }).Object);

            _mapperMock.Setup(m => m.Map<ReviewDto>(review))
                .Returns(new ReviewDto { Rating = review.Rating, Comment = review.Comment });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(review.Rating, result.Rating);
            Assert.Equal(review.Comment, result.Comment);
        }

        [Fact]
        public async Task Handle_WithInvalidFilmId_ThrowsNotFoundException()
        {
            // Arrange
            var query = new GetUserReviewForFilmQuery
            {
                FilmId = 1
            };

            _identityServiceMock.Setup(s => s.GetCurrentUserAsync())
                .ReturnsAsync(new User { Id = 1 });

            var film = new Film { Id = 1 };

            _dbContextMock.Setup(d => d.Films)
                .Returns(MockHelper.CreateMockDbSet<Film>(new List<Film> { film }).Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}