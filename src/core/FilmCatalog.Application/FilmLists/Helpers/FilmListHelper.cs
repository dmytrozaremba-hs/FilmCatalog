using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Common;
using FilmCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace FilmCatalog.Application.FilmLists.Helpers;

internal static class FilmListHelper
{
    public static async Task<bool> FilmIncludedInWatchedList(IApplicationDbContext context, User user, Film film, CancellationToken cancellationToken)
    {
        if (user == null || film == null) { return false; }

        var result = false;

        var watchedFilmList =
            await context.FilmLists.Include(x => x.Films)
                .Where(x => x.Id == user.WatchedId)
                .SingleOrDefaultAsync(cancellationToken);
        if (watchedFilmList != null)
        {
            result = watchedFilmList.Films.Contains(film);
        }

        return result;
    }

    public static async Task<bool> FilmIncludedInWatchLaterList(IApplicationDbContext context, User user, Film film, CancellationToken cancellationToken)
    {
        if (user == null || film == null) { return false; }

        var result = false;

        var watchLaterFilmList =
            await context.FilmLists.Include(x => x.Films)
                .Where(x => x.Id == user.WatchLaterId)
                .SingleOrDefaultAsync(cancellationToken);
        if (watchLaterFilmList != null)
        {
            result = watchLaterFilmList.Films.Contains(film);
        }

        return result;
    }


    public static async Task<bool> FilmIncludedInWatchedList(IApplicationDbContext context, User user, int filmId, CancellationToken cancellationToken)
    {
        if (user == null || filmId == 0) { return false; }

        var film = await context.Films.FirstOrDefaultAsync(x => x.Id == filmId, cancellationToken);

        return await FilmIncludedInWatchedList(context, user, film, cancellationToken);
    }

    public static async Task<bool> FilmIncludedInWatchLaterList(IApplicationDbContext context, User user, int filmId, CancellationToken cancellationToken)
    {
        if (user == null || filmId == 0) { return false; }

        var film = await context.Films.FirstOrDefaultAsync(x => x.Id == filmId, cancellationToken);

        return await FilmIncludedInWatchLaterList(context, user, film, cancellationToken);
    }
}
