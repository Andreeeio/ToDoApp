using MediatR;

namespace ToDoApp.Application.Users.Command.ChangePasswordWithToken;

public class ChangePasswordWithTokenCommand(string Email, string Password) : IRequest
{
    public string email = Email;
    public string password = Password;
}
