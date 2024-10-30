using FluentValidation;
using System.Text.RegularExpressions;

namespace ToDoApp.Application.Users.Command.AddUserCommand;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.FirstName).
            NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.LastName).
            NotEmpty()
            .MinimumLength(3);


        RuleFor(x => x.Email).
            NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(new Regex(@"^\d{9}$"))
            .WithMessage("Unvalid phone");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.");


    }
}
