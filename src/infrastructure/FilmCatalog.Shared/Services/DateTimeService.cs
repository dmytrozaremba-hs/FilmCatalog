using FilmCatalog.Application.Common.Interfaces;

namespace FilmCatalog.Shared.Services;

public class DateTimeService : IDateTime
{
    //public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Today => DateTime.Now.Date;

    //public DateTime UtcToday => UtcNow.Date;
}
