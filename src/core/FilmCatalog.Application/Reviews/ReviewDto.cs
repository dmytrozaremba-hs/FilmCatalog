using AutoMapper;
using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Application.Genres;
using FilmCatalog.Domain.Entities;
using System.Globalization;

namespace FilmCatalog.Application.Reviews;

public class ReviewDto : IMapFrom<Review>
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; }

    public int FilmId { get; set; }

    public string FilmTitle { get; set; }

    public string Comment { get; set; } = string.Empty;

    public int Rating { get; set; }

    public string OnDateFormatted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Review, ReviewDto>()
            .ForMember(d => d.Username, opt => opt.MapFrom(s => s.User.Username))
            .ForMember(d => d.FilmTitle, opt => opt.MapFrom(s => s.Film.Title))
            .ForMember(d => d.OnDateFormatted, opt => opt.MapFrom(s => s.CreatedAt.ToString("MMM d, yyyy", CultureInfo.InvariantCulture)));
    }
}
