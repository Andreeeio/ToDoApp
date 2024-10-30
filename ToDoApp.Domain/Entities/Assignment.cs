namespace ToDoApp.Domain.Entities;

public class Assignment
{
    public int Id { get; set; }
    public int User_Id { get; set; }
    public string? Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateOnly Created { get; set; }
    public DateOnly Expired { get; set; }
    public User User { get; set; } = default!;

}
