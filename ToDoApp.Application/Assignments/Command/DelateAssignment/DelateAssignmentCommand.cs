using MediatR;

namespace ToDoApp.Application.Assignments.Command.DeleteAssignment;

public class DelateAssignmentCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}
