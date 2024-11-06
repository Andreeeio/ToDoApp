namespace ToDoApp.Application.Users.DTO;
public record CurrentUser(int id,
    string email,
    bool isEmailConfirmed,
    IEnumerable<string> roles)
{
    public bool IsInRole(string role) => roles.Contains(role);
}
