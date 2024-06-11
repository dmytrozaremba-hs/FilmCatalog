using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using MediatR;
using FilmCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace FilmCatalog.Application.FilmLists.Commands.ToggleWatchedByUser;

public class ToggleWatchedByUserCommand : IRequest
{
    public int FilmId { get; set; }
}

public class ToggleWatchedByUserCommandHandler : IRequestHandler<ToggleWatchedByUserCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public ToggleWatchedByUserCommandHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task Handle(ToggleWatchedByUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetCurrentUserAsync() ?? throw new ForbiddenAccessException();

        var film =
            await _context.Films
                .Where(x => x.Id == request.FilmId)
                .SingleOrDefaultAsync(cancellationToken);

        if (film == null)
        {
            throw new NotFoundException(nameof(Film), request.FilmId);
        }

        var filmList =
            await _context.FilmLists.Include(x => x.Films)
                .Where(x => x.Id == user.WatchedId)
                .SingleOrDefaultAsync(cancellationToken);

        if (filmList == null)
        {
            throw new NotFoundException("Watched list for User", user.Id);
        }

        if (filmList.Films.Contains(film))
        {
            // remove
            filmList.Films.Remove(film);
        }
        else
        {
            // add
            filmList.Films.Add(film);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
