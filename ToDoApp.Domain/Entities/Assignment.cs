namespace ToDoApp.Domain.Entities;

public class Assignment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public bool Completed { get; set; }
    public DateTime Created { get; set; }
    public DateTime Expired { get; set; }
    public User User { get; set; } = default!;

}
