using ToDoApp.Application.Users.DTO;

namespace ToDoApp.Application.Interfaces;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}
