using MediatR;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Users.Command.AddUserCommand;

public class AddUserCommand : IRequest<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Password{ get; set; } = string.Empty;
}
