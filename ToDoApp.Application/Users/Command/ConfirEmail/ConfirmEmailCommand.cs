using MediatR;

namespace ToDoApp.Application.Users.Command.ConfirEmail;

public class ConfirmEmailCommand(string token, string email) : IRequest
{
    public string Token { get; set; } = token;
    public string Email { get; set; } = email;
}
