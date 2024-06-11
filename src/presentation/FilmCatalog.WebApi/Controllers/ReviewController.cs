using FilmCatalog.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using FilmCatalog.Application.Reviews;
using FilmCatalog.Application.Reviews.Queries.GetAll;
using FilmCatalog.Application.Reviews.Queries.GetUserReviewForFilm;
using FilmCatalog.Application.Reviews.Commands.UpsertUserReviewForFilm;

namespace FilmCatalog.WebApi.Controllers;

public class ReviewController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ReviewDto>>> Get([FromQuery] GetAllReviewsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("ForFilmByUser")]
    public async Task<ActionResult<ReviewDto>> GetForFilmByUser([FromQuery] GetUserReviewForFilmQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("ForFilmByUser")]
    public async Task<ActionResult<ReviewDto>> UpsertForFilmByUser(UpsertUserReviewForFilmCommand command)
    {
        return await Mediator.Send(command);
    }

}
