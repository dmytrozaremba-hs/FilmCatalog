using AutoMapper;
using FilmCatalog.Application.Common.Exceptions;
using FilmCatalog.Application.Common.Interfaces;
using FilmCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.Application.Persons.Queries.Get;

public class GetPersonQuery : IRequest<PersonDto>
{
    public int Id { get; set; }
}

public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPersonQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == 0)
        {
            return new PersonDto();
        }

        var person =
            await _context.Persons
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

        if (person == null)
        {
            throw new NotFoundException(nameof(Person), request.Id);
        }

        var result = _mapper.Map<PersonDto>(person);

        return result;
    }
}