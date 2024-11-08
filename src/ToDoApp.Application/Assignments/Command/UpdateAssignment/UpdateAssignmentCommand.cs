using MediatR;

namespace ToDoApp.Application.Assignments.Command.UpdateAssignment;

public class UpdateAssignmentCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty ;
    public bool? Completed { get; set; } = null;
    public DateTime? Expired { get; set; } = null ;
}
