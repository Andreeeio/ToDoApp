using ToDoApp.Domain.Constants;

namespace ToDoApp.Domain.Interfaces;

public interface IAssignmentAuthorization
{
    bool Authorize(ResourceOperation operation);
}
