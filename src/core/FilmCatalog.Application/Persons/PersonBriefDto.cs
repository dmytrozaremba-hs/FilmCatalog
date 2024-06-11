using AutoMapper;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Domain.Entities;

namespace FilmCatalog.Application.Persons;

public class PersonBriefDto : IMapFrom<Person>
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Person, PersonBriefDto>();
    }
}
