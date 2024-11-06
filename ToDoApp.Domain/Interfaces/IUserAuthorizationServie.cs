using ToDoApp.Domain.Constants;

namespace ToDoApp.Domain.Interfaces;

public interface IUserAuthorizationServie
{
    bool Authorize(ResourceOperation operation);
}