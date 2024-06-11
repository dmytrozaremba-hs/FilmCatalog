using FilmCatalog.Application.Identity;
using FilmCatalog.Application.Identity.Commands.Login;
using FilmCatalog.Application.Identity.Commands.Signup;
using Microsoft.AspNetCore.Mvc;

namespace FilmCatalog.WebApi.Controllers;

public class IdentityController : ApiControllerBase
{

    [HttpPost("[action]")]
    public async Task<ActionResult<AuthenticationResultDto>> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<AuthenticationResultDto>> Signup(SignupCommand command)
    {
        return await Mediator.Send(command);
    }
}
