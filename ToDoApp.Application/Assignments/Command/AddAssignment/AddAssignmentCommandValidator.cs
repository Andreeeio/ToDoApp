﻿using FluentValidation;

namespace ToDoApp.Application.Assignments.Command.AddAssignment;

public class AddAssignmentCommandValidator : AbstractValidator<AddAssignmentCommand>
{
    public AddAssignmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3);

    }
}
