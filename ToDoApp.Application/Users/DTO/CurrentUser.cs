namespace ToDoApp.Application.Users.DTO;
public record CurrentUser(int Id,
    string Email,
    bool IsEmailConfirmed,
    IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
