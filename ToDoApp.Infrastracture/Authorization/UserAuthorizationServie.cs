using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Services;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Infrastracture.Authorization;

public class UserAuthorizationServie(IUserContext userContext) : IUserAuthorizationServie
{
    private readonly IUserContext _userContext = userContext;

    public bool Authorize(ResourceOperation operation)
    {
        var user = _userContext.GetCurrentUser();

        if (user == null)
        {
            throw new UnauthorizedExeption("User not found");
        }
        if(operation == ResourceOperation.Update)
        {
            return true;
        } 
        if(operation == ResourceOperation.Delete )
        {
            return true;
        } 

        return false; 
    }

}
