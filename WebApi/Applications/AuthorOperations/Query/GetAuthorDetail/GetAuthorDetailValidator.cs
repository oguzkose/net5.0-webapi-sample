using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Query.GetAuthorDetail
{
    public class GetAuthorDetailValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailValidator()
        {
            RuleFor(query => query.AuthorId).GreaterThan(0);
        }
    }
}