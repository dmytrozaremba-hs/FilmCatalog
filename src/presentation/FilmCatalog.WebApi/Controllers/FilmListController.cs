using FilmCatalog.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

using FilmCatalog.Application.FilmLists.Queries.GetWatchedByUser;
using FilmCatalog.Application.FilmLists.Queries.GetWatchLaterByUser;
using FilmCatalog.Application.Films.Queries.GetAll;
using FilmCatalog.Application.FilmLists.Commands.ToggleWatchedByUser;
using FilmCatalog.Application.FilmLists.Commands.ToggleWatchLaterByUser;

namespace FilmCatalog.WebApi.Controllers;

public class FilmListController : ApiControllerBase
{
    [HttpGet("WatchedByUser")]
    public async Task<ActionResult<PaginatedList<FilmBriefDto>>> GetWatchedByUser([FromQuery] GetWatchedByUserQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("WatchLaterByUser")]
    public async Task<ActionResult<PaginatedList<FilmBriefDto>>> GetWatchLaterByUser([FromQuery] GetWatchLaterByUserQuery query)
    {
        return await Mediator.Send(query);
    }


    [HttpPost("ToggleWatchedByUser")]
    public async Task<ActionResult> ToggleWatchedByUser(ToggleWatchedByUserCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPost("ToggleWatchLaterByUser")]
    public async Task<ActionResult> ToggleWatchLaterByUser(ToggleWatchLaterByUserCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

}