using MediatR;

namespace ToDoApp.Application.Assignments.Command.DeleteAssignment;

public class DeleteAssignmentCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}
