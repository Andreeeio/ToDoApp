using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces;

public interface IAssignmentAuthorizationService
{
    bool Authorize(ResourceOperation operation, Assignment assignment);
}