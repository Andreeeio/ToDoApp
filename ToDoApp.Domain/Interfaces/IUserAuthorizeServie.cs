using ToDoApp.Domain.Constants;

namespace ToDoApp.Domain.Interfaces;

public interface IUserAuthorizeServie
{
    bool Authorize(ResourceOperation operation);
}
