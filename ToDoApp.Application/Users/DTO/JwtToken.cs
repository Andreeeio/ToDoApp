namespace ToDoApp.Application.Users.DTO;

public class JwtToken(string token)
{
    public string Token { get; set; } = token;
}
