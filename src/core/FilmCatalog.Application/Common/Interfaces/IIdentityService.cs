using FilmCatalog.Application.Common.Models;
using FilmCatalog.Domain.Entities;
using FilmCatalog.Domain.Enums;

namespace FilmCatalog.Application.Common.Interfaces;

public interface IIdentityService
{
    int? GetCurrentUserId();

    Task<User> GetCurrentUserAsync();

    Task<(Result Result, AuthenticationResult AuthenticationResult)> CreateUserAsync(string email, string password, string username, Role role);

    Task<(Result Result, AuthenticationResult AuthenticationResult)> LoginAsync(string email, string password);
}
