using FilmCatalog.Application.Common.Mappings;
using FilmCatalog.Domain.Entities;

namespace FilmCatalog.Application.Tags;

public class TagDto : IMapFrom<Tag>
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}