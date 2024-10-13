namespace ToDoApp.Domain.Entities;

public class Roles
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<User> Users { get; set; } = default!;
}
