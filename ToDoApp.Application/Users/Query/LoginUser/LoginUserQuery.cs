using MediatR;
using ToDoApp.Application.Users.DTO;

namespace ToDoApp.Application.Users.Query.LoginUser;

public class LoginUserQuery(string Login, string Password) : IRequest<JwtToken>
{
    public string login { get; set; } = Login;  
    public string password { get; set; } = Password;    
}
