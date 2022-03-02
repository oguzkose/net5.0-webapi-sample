using FluentValidation;

namespace WebApi.Applications.BookOperations.Command.DeleteBook
{
    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command=>command.BookId).NotEmpty().GreaterThan(0);
        }
    }
}