using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Entities;
using FilmCatalog.Identity.Helpers;
using FilmCatalog.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FilmCatalog.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureIdentity(this IServiceCollection services, IConfiguration config)
    {
        var authSettings = new AuthSettings();
        config.Bind(nameof(AuthSettings), authSettings);
        services.AddSingleton(authSettings);

        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddScoped<IIdentityService, IdentityService>();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true
        };

        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

        services.AddAuthorization();


        return services;
    }
}
