using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmCatalog.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureShared(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();

        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}