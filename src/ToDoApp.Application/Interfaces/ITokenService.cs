using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Interfaces;

public interface ITokenService
{
    string GetToken(User user);
}
