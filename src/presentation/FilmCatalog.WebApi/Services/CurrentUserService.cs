using FilmCatalog.Application.Common.Interfaces;
using System.Security.Claims;

namespace FilmCatalog.WebApi.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            if (int.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("id"), out int userId))
            {
                return userId;
            }
            return null;
        }
    }
}
