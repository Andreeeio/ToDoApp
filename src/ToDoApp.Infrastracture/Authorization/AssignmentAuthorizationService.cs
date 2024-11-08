using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Infrastracture.Authorization;

public class AssignmentAuthorizationService(IUserContext userContext) : IAssignmentAuthorizationService
{
    private readonly IUserContext _userContext = userContext;

    public bool Authorize(ResourceOperation resource, Assignment assignment)
    {
        var user = _userContext.GetCurrentUser();

        if (user != null && user.isEmailConfirmed == false)
        {
            throw new UnauthorizedExeption("Unconfirmed user");
        }
        else if (resource == ResourceOperation.Create)
        {
            return true;
        }
        else if (resource == ResourceOperation.Read && user.email != null) 
        {
            return true;
        }
        else if(resource == ResourceOperation.Delete && user.id == assignment.UserId)
        {
            return true;
        }
        else if (resource == ResourceOperation.Update && user.id == assignment.UserId)
        {
            return true;
        }
        return false;
    }
}