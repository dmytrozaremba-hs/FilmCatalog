using FluentValidation;

namespace FilmCatalog.Application.Films.Queries.GetAll;

public class GetAllFilmsQueryValidator:AbstractValidator<GetAllFilmsQuery>
{
    public GetAllFilmsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
