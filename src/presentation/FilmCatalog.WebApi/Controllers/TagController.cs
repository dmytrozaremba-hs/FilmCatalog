using FilmCatalog.Application.Common.Models;
using FilmCatalog.Application.Tags.Queries.GetAll;
using FilmCatalog.Application.Tags;
using Microsoft.AspNetCore.Mvc;
using FilmCatalog.Application.Tags.Commands.Create;
using FilmCatalog.Application.Tags.Commands.Delete;
using FilmCatalog.Application.Tags.Commands.Update;
using FilmCatalog.Application.Tags.Queries.Get;

namespace FilmCatalog.WebApi.Controllers;

public class TagController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<TagDto>>> Get([FromQuery] GetAllTagsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> Get(int id)
    {
        return await Mediator.Send(new GetTagQuery { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTagCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateTagCommand command)
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
        await Mediator.Send(new DeleteTagCommand { Id = id });

        return NoContent();
    }
}
