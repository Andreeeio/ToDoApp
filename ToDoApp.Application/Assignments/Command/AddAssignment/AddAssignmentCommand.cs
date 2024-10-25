using MediatR;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Assignments.Command.AddAssignment;

public class AddAssignmentCommand : IRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public DateTime Created { get; set; }
    public DateTime Expired { get; set; }
}
