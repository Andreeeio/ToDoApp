using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Assignments.DTO;

public class AssignmentDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public bool Completed { get; set; }
    public DateTime Created { get; set; }
    public DateTime Expired { get; set; }
}
