namespace FilmCatalog.Domain.Entities;

public class AuthenticationResult
{
    public User User { get; init; }

    public string Token { get; init; }
}
