using FluentValidation;

namespace WebApi.Applications.BookOperations.Query.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query => query.BookId).NotEmpty().GreaterThan(0);
        }
    }
}