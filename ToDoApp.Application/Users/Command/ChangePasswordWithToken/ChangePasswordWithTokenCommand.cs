using MediatR;

namespace ToDoApp.Application.Users.Command.ChangePasswordWithToken;

public class ChangePasswordWithTokenCommand(string email, string password, string token) : IRequest
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public string Token { get; set; } = token;
}
