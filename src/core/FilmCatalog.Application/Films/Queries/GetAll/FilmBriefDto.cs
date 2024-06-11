using AutoMapper;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Application.Genres;
using FilmCatalog.Domain.Entities;
using System.Globalization;

namespace FilmCatalog.Application.Films.Queries.GetAll;

public class FilmBriefDto : IMapFrom<Film>
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string ShortDescription { get; set; }

    public string Year { get; set; }

    public string PosterUrl { get; set; }

    public IList<int> Genres { get; set; } = new List<int>();

    public int DurationInMinutes { get; set; }

    public double AverageRating { get; set; }

    public int NumberOfVotes { get; set; }

    public bool IncludedInWatchedList { get; set; }

    public bool IncludedInWatchLaterList { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Film, FilmBriefDto>()
            .ForMember(d => d.ShortDescription, opt => opt.MapFrom(s => s.Description.Length > 255 ? $"{s.Description.Substring(0, 252)} ..." : s.Description))
            .ForMember(d => d.Year, opt => opt.MapFrom(s => s.ReleaseDate.ToString("yyyy", CultureInfo.InvariantCulture)))
            .ForMember(d => d.Genres, opt => opt.MapFrom(s => s.Genres.Select(x => x.Id)));
    }
}
