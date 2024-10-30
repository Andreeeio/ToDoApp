using MediatR;

namespace ToDoApp.Application.Assignments.Command.UpdateAssignment;

public class UpdateAssignmentCommand(int Id) : IRequest
{
    public int id { get; set; } = Id;
    public string? name { get; set; } = string.Empty;
    public string? description { get; set; } = string.Empty ;
    public bool? completed { get; set; } = null;
    public DateTime? expired { get; set; } = null ;
}
