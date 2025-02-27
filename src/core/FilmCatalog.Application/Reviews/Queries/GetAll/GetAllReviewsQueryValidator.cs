using FluentValidation;

namespace FilmCatalog.Application.Reviews.Queries.GetAll;

public class GetAllReviewsQueryValidator:AbstractValidator<GetAllReviewsQuery>
{
    public GetAllReviewsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
