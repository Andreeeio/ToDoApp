using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Services;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Infrastracture.Authorization;

public class UserAuthorizeServie(IUserContext userContext) : IUserAuthorizeServie
{
    private readonly IUserContext _userContext = userContext;

    public bool Authorize(ResourceOperation operation)
    {
        var user = _userContext.GetCurrentUser();

        if(operation == ResourceOperation.Update && user != null)
        {
            return true;
        } 

        return false; 
    }

}
