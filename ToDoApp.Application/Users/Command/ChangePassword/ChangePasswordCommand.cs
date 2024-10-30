using MediatR;

namespace ToDoApp.Application.Users.Command.ChangePassword;

public class ChangePasswordCommand(string op, string np) : IRequest
{
    public string oldPassword { get; set; } = op;
    public string newPassword { get; set; } = np;
}
