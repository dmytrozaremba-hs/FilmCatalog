using AutoMapper;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Application.Films.Queries.GetAll;
using FilmCatalog.Application.Persons;
using FilmCatalog.Domain.Entities;
using System.Globalization;

namespace FilmCatalog.Application.Films.Queries.Get;

public class FilmDto : IMapFrom<Film>
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Year { get; set; }

    public string PosterUrl { get; set; }

    public IList<int> Genres { get; set; } = new List<int>();

    public IList<int> Tags { get; set; } = new List<int>();

    public IList<PersonBriefDto> Directors { get; set; } = new List<PersonBriefDto>();

    public IList<PersonBriefDto> Producers { get; set; } = new List<PersonBriefDto>();

    public IList<PersonBriefDto> Actors { get; set; } = new List<PersonBriefDto>();

    public int DurationInMinutes { get; set; }

    public double AverageRating { get; set; }

    public int NumberOfVotes { get; set; }

    public bool IncludedInWatchedList { get; set; }

    public bool IncludedInWatchLaterList { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Film, FilmDto>()
            .ForMember(d => d.Year, opt => opt.MapFrom(s => s.ReleaseDate.ToString("yyyy", CultureInfo.InvariantCulture)))
            .ForMember(d => d.Genres, opt => opt.MapFrom(s => s.Genres.Select(x => x.Id)))
            .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.Tags.Select(x => x.Id)))
            .ForMember(d => d.Directors, opt => opt.MapFrom(s => s.Directors))
            .ForMember(d => d.Producers, opt => opt.MapFrom(s => s.Producers))
            .ForMember(d => d.Actors, opt => opt.MapFrom(s => s.Actors));
    }
}
