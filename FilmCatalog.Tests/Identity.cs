using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Entities;
using FilmCatalog.Domain.Enums;
using FilmCatalog.Identity.Helpers;
using FilmCatalog.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FilmCatalog.Tests
{
    public class IdentityServiceTests
    {
        private readonly Mock<IApplicationDbContext> _dbContextMock;
        private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly AuthSettings _authSettings;

        public IdentityServiceTests()
        {
            _dbContextMock = new Mock<IApplicationDbContext>();
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();
            _currentUserServiceMock = new Mock<ICurrentUserService>();

            _authSettings = new AuthSettings
            {
                Secret = "test-secret-key",
                TokenLifetime = TimeSpan.FromMinutes(30)
            };
        }

        // GetCurrentUserId_ReturnsCurrentUserId
        [Fact]
        public void GetCurrentUserId_ReturnsCurrentUserId()
        {
            // Arrange
            int expectedUserId = 123;
            _currentUserServiceMock.Setup(c => c.UserId).Returns(expectedUserId);

            var identityService = new IdentityService(_dbContextMock.Object, _authSettings, _passwordHasherMock.Object, _currentUserServiceMock.Object);

            // Act
            int? currentUserId = identityService.GetCurrentUserId();

            // Assert
            Assert.Equal(expectedUserId, currentUserId);
        }

        // GetCurrentUserAsync_UserIdNull_ReturnsNull
        [Fact]
        public async Task GetCurrentUserAsync_UserIdNull_ReturnsNull()
        {
            // Arrange
            _currentUserServiceMock.Setup(c => c.UserId).Returns((int?)null);

            var identityService = new IdentityService(_dbContextMock.Object, _authSettings, _passwordHasherMock.Object, _currentUserServiceMock.Object);

            // Act
            User currentUser = await identityService.GetCurrentUserAsync();

            // Assert
            Assert.Null(currentUser);
        }

        // GetCurrentUserAsync_UserIdNotNull_ReturnsUser
        [Fact]
        public async Task GetCurrentUserAsync_UserIdNotNull_ReturnsUser()
        {
            // Arrange
            int currentUserId = 123;
            var expectedUser = new User { Id = currentUserId };
            _currentUserServiceMock.Setup(c => c.UserId).Returns(currentUserId);
            _dbContextMock.Setup(d => d.Users.FindAsync(currentUserId)).ReturnsAsync(expectedUser);

            var identityService = new IdentityService(_dbContextMock.Object, _authSettings, _passwordHasherMock.Object, _currentUserServiceMock.Object);

            // Act
            User currentUser = await identityService.GetCurrentUserAsync();

            // Assert
            Assert.Equal(expectedUser, currentUser);
        }

        // CreateUserAsync_UserAlreadyExists_ReturnsFailure
        [Fact]
        public async Task CreateUserAsync_UserAlreadyExists_ReturnsFailure()
        {
            // Arrange
            string email = "test@example.com";
            string password = "test-password";
            string username = "test-username";
            Role role = Role.Regular;

            var existingUser = new User { Email = email };
            var users = new List<User> { existingUser };

            var userDbSetMock = new Mock<DbSet<User>>();
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.Provider).Returns(users.AsQueryable().Provider);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.Expression).Returns(users.AsQueryable().Expression);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.ElementType).Returns(users.AsQueryable().ElementType);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            _dbContextMock.Setup(d => d.Users).Returns(userDbSetMock.Object);

            var identityService = new IdentityService(_dbContextMock.Object, _authSettings, _passwordHasherMock.Object, _currentUserServiceMock.Object);

            // Act
            var (result, authenticationResult) = await identityService.CreateUserAsync(email, password, username, role);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(authenticationResult);
        }

        // CreateUserAsync_ValidUser_ReturnsSuccessAndAuthenticationResult
        [Fact]
        public async Task CreateUserAsync_ValidUser_ReturnsSuccessAndAuthenticationResult()
        {
            // Arrange
            string email = "test@example.com";
            string password = "test-password";
            string username = "test-username";
            Role role = Role.Regular;

            var users = new List<User>();

            var userDbSetMock = new Mock<DbSet<User>>();
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.Provider).Returns(users.AsQueryable().Provider);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.Expression).Returns(users.AsQueryable().Expression);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.ElementType).Returns(users.AsQueryable().ElementType);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            _dbContextMock.Setup(d => d.Users).Returns(userDbSetMock.Object);

            var identityService = new IdentityService(_dbContextMock.Object, _authSettings, _passwordHasherMock.Object, _currentUserServiceMock.Object);

            // Act
            var (result, authenticationResult) = await identityService.CreateUserAsync(email, password, username, role);

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(authenticationResult);
            Assert.NotNull(authenticationResult.User);
            Assert.Equal(email, authenticationResult.User.Email);
            Assert.Equal(username, authenticationResult.User.Username);
            Assert.Equal(role, authenticationResult.User.Role);
            Assert.NotEmpty(authenticationResult.Token);
        }

        // LoginAsync_UserNotFound_ReturnsFailure
        [Fact]
        public async Task LoginAsync_UserNotFound_ReturnsFailure()
        {
            // Arrange
            string email = "test@example.com";
            string password = "test-password";

            var users = new List<User>();

            var userDbSetMock = new Mock<DbSet<User>>();
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.Provider).Returns(users.AsQueryable().Provider);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.Expression).Returns(users.AsQueryable().Expression);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.ElementType).Returns(users.AsQueryable().ElementType);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            _dbContextMock.Setup(d => d.Users).Returns(userDbSetMock.Object);

            var identityService = new IdentityService(_dbContextMock.Object, _authSettings, _passwordHasherMock.Object, _currentUserServiceMock.Object);

            // Act
            var (result, authenticationResult) = await identityService.LoginAsync(email, password);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(authenticationResult);
        }

        // LoginAsync_InvalidPassword_ReturnsFailure
        [Fact]
        public async Task LoginAsync_InvalidPassword_ReturnsFailure()
        {
            // Arrange
            string email = "test@example.com";
            string password = "test-password";
            string hashedPassword = "hashed-password";

            var users = new List<User>
            {
                new User { Email = email, HashedPassword = hashedPassword }
            };

            var userDbSetMock = new Mock<DbSet<User>>();
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.Provider).Returns(users.AsQueryable().Provider);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.Expression).Returns(users.AsQueryable().Expression);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.ElementType).Returns(users.AsQueryable().ElementType);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            _dbContextMock.Setup(d => d.Users).Returns(userDbSetMock.Object);
            _passwordHasherMock.Setup(p => p.VerifyHashedPassword(It.IsAny<User>(), hashedPassword, password))
                .Returns(PasswordVerificationResult.Failed);

            var identityService = new IdentityService(_dbContextMock.Object, _authSettings, _passwordHasherMock.Object, _currentUserServiceMock.Object);

            // Act
            var (result, authenticationResult) = await identityService.LoginAsync(email, password);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(authenticationResult);
        }

        // LoginAsync_ValidCredentials_ReturnsSuccessAndAuthenticationResult
        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsSuccessAndAuthenticationResult()
        {
            // Arrange
            string email = "test@example.com";
            string password = "test-password";
            string hashedPassword = "hashed-password";
            int userId = 123;

            var user = new User { Id = userId, Email = email, HashedPassword = hashedPassword };

            var users = new List<User>
            {
                user
            };

            var userDbSetMock = new Mock<DbSet<User>>();
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.Provider).Returns(users.AsQueryable().Provider);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.Expression).Returns(users.AsQueryable().Expression);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.ElementType).Returns(users.AsQueryable().ElementType);
            userDbSetMock.As<IQueryable<User>>().Setup(u => u.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            _dbContextMock.Setup(d => d.Users).Returns(userDbSetMock.Object);
            _passwordHasherMock.Setup(p => p.VerifyHashedPassword(It.IsAny<User>(), hashedPassword, password))
                .Returns(PasswordVerificationResult.Success);

            var identityService = new IdentityService(_dbContextMock.Object, _authSettings, _passwordHasherMock.Object, _currentUserServiceMock.Object);

            // Act
            var (result, authenticationResult) = await identityService.LoginAsync(email, password);

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(authenticationResult);
            Assert.NotNull(authenticationResult.User);
            Assert.Equal(user, authenticationResult.User);
            Assert.NotEmpty(authenticationResult.Token);
        }
    }
}
