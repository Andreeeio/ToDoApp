using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Users.DTO;

public class UserDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Phone { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public List<Assignment> Assignments { get; set; } = default!;
    public List<Roles> Roles { get; set; } = default!;
}
