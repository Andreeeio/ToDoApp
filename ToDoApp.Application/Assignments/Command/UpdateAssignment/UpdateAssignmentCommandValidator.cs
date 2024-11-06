using FluentValidation;
using Microsoft.AspNetCore.Rewrite;

namespace ToDoApp.Application.Assignments.Command.UpdateAssignment;

public class UpdateAssignmentCommandValidator : AbstractValidator<UpdateAssignmentCommand>
{
    public UpdateAssignmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3);

        RuleFor(x => x.Expired)
            .GreaterThan(DateTime.UtcNow);
    }
}
