using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Domain.Entities;

namespace FilmCatalog.Application.Genres;

public class GenreDto : IMapFrom<Genre>
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}