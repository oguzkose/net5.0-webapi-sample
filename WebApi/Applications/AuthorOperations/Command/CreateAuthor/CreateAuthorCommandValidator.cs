using System;
using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(1).MaximumLength(50);
            RuleFor(x => x.Model.Surname).NotEmpty().MinimumLength(1).MaximumLength(50);
            RuleFor(x => x.Model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);

        }
    }
}