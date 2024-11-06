using MediatR;
using ToDoApp.Application.Users.DTO;

namespace ToDoApp.Application.Users.Query.LoginUser;

public class LoginUserQuery(string login, string password) : IRequest<JwtToken>
{
    public string Login { get; set; } = login;  
    public string Password { get; set; } = password;    
}