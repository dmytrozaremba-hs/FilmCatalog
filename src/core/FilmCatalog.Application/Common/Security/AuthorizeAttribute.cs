using FilmCatalog.Domain.Enums;

namespace FilmCatalog.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeAttribute : Attribute
{
    public AuthorizeAttribute() { }

    public Role Role { get; set; } = Role.None;
}
