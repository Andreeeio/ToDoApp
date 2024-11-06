using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Users.DTO;

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

        var id = user.FindFirst(u => u.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(u => u.Type == ClaimTypes.Email)!.Value;
        var confirmed = user.FindFirst(u => u.Type == ClaimTypes.Actor)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value).ToList();

        return new CurrentUser(int.Parse(id), email, bool.Parse(confirmed), roles);
    }
}