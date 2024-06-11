using AutoMapper;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Domain.Entities;

namespace FilmCatalog.Application.Persons;

public class PersonDto : IMapFrom<Person>
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public bool IsDirector { get; set; }

    public bool IsProducer { get; set; }

    public bool IsActor { get; set; }

    public DateTime BirthDate { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Person, PersonDto>();
    }
}
