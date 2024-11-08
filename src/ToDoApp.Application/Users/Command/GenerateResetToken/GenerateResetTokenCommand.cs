using MediatR;

namespace ToDoApp.Application.Users.Command.GenerateResetToken;

public class GenerateResetTokenCommand(string Email) : IRequest
{
    public string email {  get; set; } = Email;
}
