using FluentValidation;

namespace FilmCatalog.Application.Tags.Queries.GetAll;

public class GetAllTagsQueryValidator:AbstractValidator<GetAllTagsQuery>
{
    public GetAllTagsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
