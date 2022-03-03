using FluentValidation;

namespace WebApi.Applications.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.Name).NotNull().MinimumLength(1).MaximumLength(50);
            RuleFor(command => command.Model.IsActive).Must(x => x == true || x == false);
        }
    }
}