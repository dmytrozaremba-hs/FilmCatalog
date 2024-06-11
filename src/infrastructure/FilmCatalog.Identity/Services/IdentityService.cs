using Microsoft.EntityFrameworkCore;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Application.Common.Models;
using FilmCatalog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FilmCatalog.Identity.Helpers;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using FilmCatalog.Domain.Enums;

namespace FilmCatalog.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly IApplicationDbContext _context;
    private readonly AuthSettings _authSettings;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ICurrentUserService _currentUserService;

    public IdentityService(IApplicationDbContext context, AuthSettings authSettings, IPasswordHasher<User> passwordHasher, ICurrentUserService currentUserService)
    {
        _context = context;
        _authSettings = authSettings;
        _passwordHasher = passwordHasher;
        _currentUserService = currentUserService;
    }

    public int? GetCurrentUserId()
    {
        return _currentUserService.UserId;
    }

    public async Task<User> GetCurrentUserAsync()
    {
        var userId = GetCurrentUserId();

        if (userId == null)
            return null;

        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(e => e.Id == userId);

        return user;
    }

    public async Task<(Result Result, AuthenticationResult AuthenticationResult)> CreateUserAsync(string email, string password, string username, Role role)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(e => e.Email == email);

        if (user != null)
        {
            return (Result.Failure(new[] { (nameof(email), "This email address is already used") }), null);
        }

        user = new User { Email = email, Username = username, Role = role, Watched = new(), WatchLater = new() };
        user.HashedPassword = _passwordHasher.HashPassword(user, password);



        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return (Result.Failure(new[] { ex.Message }), null);
        }

        return (Result.Success(), new AuthenticationResult { User = user, Token = GenerateAuthenticationToken(user) });
    }

    public async Task<(Result Result, AuthenticationResult AuthenticationResult)> LoginAsync(string email, string password)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(e => e.Email == email);

        if (user == null)
        {
            return (Result.Failure(new[] { (nameof(email), "No user found for this email address") }), null);
        }

        if (PasswordVerificationResult.Failed == _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password))
        {
            return (Result.Failure(new[] { (nameof(password), "Invalid password") }), null);
        }

        return (Result.Success(), new AuthenticationResult { User = user, Token = GenerateAuthenticationToken(user) });
    }

    private string GenerateAuthenticationToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authSettings.Secret);

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("id", user.Id.ToString())
            };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_authSettings.TokenLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

}
