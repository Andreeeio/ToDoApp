using MediatR;

namespace ToDoApp.Application.Users.Command.ChangePassword;

public class ChangePasswordCommand(string op, string np) : IRequest
{
    public string OldPassword { get; set; } = op;
    public string NewPassword { get; set; } = np;
}
