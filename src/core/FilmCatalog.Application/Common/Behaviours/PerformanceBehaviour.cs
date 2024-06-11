﻿using System.Diagnostics;
using FilmCatalog.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FilmCatalog.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly IIdentityService _identityService;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        IIdentityService identityService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var user = await _identityService.GetCurrentUserAsync();

            var userName = string.Empty;

            if (user != null)
            {
                userName = $"{user.Username} ({user.Email})";
            }

            _logger.LogWarning("FilmCatalog Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, user?.Id.ToString() ?? string.Empty, userName, request);
        }

        return response;
    }
}
