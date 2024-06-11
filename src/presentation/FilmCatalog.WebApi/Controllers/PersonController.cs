using FilmCatalog.Application.Common.Models;
using FilmCatalog.Application.Persons.Commands.Create;
using FilmCatalog.Application.Persons.Commands.Delete;
using FilmCatalog.Application.Persons.Commands.Update;
using FilmCatalog.Application.Persons;
using FilmCatalog.Application.Persons.Queries.Get;
using FilmCatalog.Application.Persons.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace FilmCatalog.WebApi.Controllers;


public class PersonController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<PersonBriefDto>>> Get([FromQuery] GetAllPersonsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> Get(int id)
    {
        return await Mediator.Send(new GetPersonQuery { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreatePersonCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdatePersonCommand command)
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
        await Mediator.Send(new DeletePersonCommand { Id = id });

        return NoContent();
    }
}