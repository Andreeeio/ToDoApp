﻿using FluentValidation;

namespace ToDoApp.Application.Users.Command.ChangePasswordWithToken;

public class ChangePasswordWithTokenCommandValidator : AbstractValidator<ChangePasswordWithTokenCommand>
{
    public ChangePasswordWithTokenCommandValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.");
    }
}
