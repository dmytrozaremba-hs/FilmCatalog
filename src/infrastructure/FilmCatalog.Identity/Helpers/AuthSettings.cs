namespace FilmCatalog.Identity.Helpers;

public class AuthSettings
{
    public string Secret { get; set; } = string.Empty;

    public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromSeconds(8 * 60 * 60);
}
