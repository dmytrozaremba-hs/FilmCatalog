using FilmCatalog.Application.Common.Models;
using FilmCatalog.Application.Genres;
using FilmCatalog.Application.Genres.Commands.Create;
using FilmCatalog.Application.Genres.Commands.Delete;
using FilmCatalog.Application.Genres.Commands.Update;
using FilmCatalog.Application.Genres.Queries.Get;
using FilmCatalog.Application.Genres.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace FilmCatalog.WebApi.Controllers;

public class GenreController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<GenreDto>>> Get([FromQuery] GetAllGenresQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> Get(int id)
    {
        return await Mediator.Send(new GetGenreQuery { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateGenreCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateGenreCommand command)
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
        await Mediator.Send(new DeleteGenreCommand { Id = id });

        return NoContent();
    }
}
