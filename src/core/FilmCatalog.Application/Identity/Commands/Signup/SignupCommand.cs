using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Enums;
using MediatR;

namespace FilmCatalog.Application.Identity.Commands.Signup;

public class SignupCommand : IRequest<AuthenticationResultDto>
{
    public string Email { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string PasswordAgain { get; set; }
}

public class SignupCommandHandler : IRequestHandler<SignupCommand, AuthenticationResultDto>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public SignupCommandHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<AuthenticationResultDto> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var (result, authenticationResult) = await _identityService.CreateUserAsync(request.Email, request.Password, request.Username, Role.Regular);

        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors);
        }

        return _mapper.Map<AuthenticationResultDto>(authenticationResult);
    }
}