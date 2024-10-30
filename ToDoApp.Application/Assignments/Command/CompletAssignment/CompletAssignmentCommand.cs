using MediatR;

namespace ToDoApp.Application.Assignments.Command.CompletAssignment;

public class CompletAssignmentCommand(int id)  : IRequest
{
    public int Id { get; set; } = id;
}
