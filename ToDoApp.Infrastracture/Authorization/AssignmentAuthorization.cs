using Microsoft.AspNetCore.Authorization;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Infrastracture.Authorization;

public class AssignmentAuthorization(IUserContext userContext) : IAssignmentAuthorization
{
    private readonly IUserContext _userContext = userContext;

    public bool Authorize(ResourceOperation resource)
    {
        var user = _userContext.GetCurrentUser();

        if (user != null && user.IsEmailConfirmed == false)
        {
            throw new UnauthorizedExeption("Unconfirmed user");
        }
        
        if (resource == ResourceOperation.Create && user != null)
        {
            return true;
        }
        return false;
    }
}
