using FluentValidation;

namespace WebApi.Applications.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(1).When(x => x.Model.Name.Trim() != string.Empty).MaximumLength(50);
            RuleFor(command => command.Model.IsActive).Must(x => x == true || x == false);
        }
    }
}