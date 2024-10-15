using AutoMapper;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Users.DTO;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users;

public class UserServices(IUserRepositories userRepositories,
    ILogger<UserServices> looger,
    IMapper mapper) : IUserServices
{
    public async Task<IEnumerable<UserDTO>> GetAllUsers() 
    {
        looger.LogInformation("Get all users");
        var users = await userRepositories.GetAllUsersAsync();
        
        var usersDtos = mapper.Map<IEnumerable<UserDTO>>(users);
        return usersDtos;
    }
}
