using FilmCatalog.Application.Common.Models;
using FilmCatalog.Application.Films.Queries.Get;
using FilmCatalog.Application.Films.Queries.GetAll;
using FilmCatalog.Application.Films.Commands.Create;
using FilmCatalog.Application.Films.Commands.Delete;
using FilmCatalog.Application.Films.Commands.Update;
using Microsoft.AspNetCore.Mvc;

namespace FilmCatalog.WebApi.Controllers;

public class FilmController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<FilmBriefDto>>> Get([FromQuery] GetAllFilmsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FilmDto>> Get(int id)
    {
        return await Mediator.Send(new GetFilmQuery { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateFilmCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateFilmCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteFilmCommand { Id = id });

        return NoContent();
    }
}
