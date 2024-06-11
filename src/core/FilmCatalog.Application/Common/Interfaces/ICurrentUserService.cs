namespace FilmCatalog.Application.Common.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; }
}