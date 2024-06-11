using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using MediatR;

namespace FilmCatalog.Application.Identity.Commands.Login;

public class LoginCommand : IRequest<AuthenticationResultDto>
{
    public string Email { get; set; }

    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationResultDto>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public LoginCommandHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<AuthenticationResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var (result, authenticationResult) = await _identityService.LoginAsync(request.Email, request.Password);

        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors);
        }

        if (authenticationResult.User.Role == Domain.Enums.Role.None) {
            throw new ForbiddenAccessException();
        }

        return _mapper.Map<AuthenticationResultDto>(authenticationResult);
    }
}