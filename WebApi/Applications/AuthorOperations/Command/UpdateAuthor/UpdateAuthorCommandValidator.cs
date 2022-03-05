using System;
using FluentValidation;

namespace WebApi.Applications.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(1).MaximumLength(50);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(1).MaximumLength(50);
            RuleFor(command => command.Model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}