using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Users.DTO;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;

namespace ToDoApp.Application.Services;

public class UserContext(IHttpContextAccessor httpContext) : IUserContext
{
    private readonly IHttpContextAccessor _httpContext = httpContext;

    public CurrentUser? GetCurrentUser()
    {
        var user = (_httpContext?.HttpContext?.User)
            ?? throw new InvalidOperationException("User context is not present");
        
        if(user.Identity == null)
        {
            return null;
        }
        int id;
        string email;
        bool confirmed;
        var _id = user.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
        if (_id == null || !int.TryParse(_id.Value, out id))
            throw new UnauthorizedExeption("Unauthorized user");
        var _email = user.FindFirst(u => u.Type == ClaimTypes.Email);
        if (_email == null)
            throw new UnauthorizedExeption("Unauthorized user");
        email = _email.Value;
        var _confirmed = user.FindFirst(u => u.Type == ClaimTypes.Actor);
        if (_confirmed == null || !bool.TryParse(_confirmed.Value, out confirmed))
            throw new UnauthorizedExeption("Unauthorized user");
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value).ToList();

        return new CurrentUser(id, email, confirmed, roles);
    }
}