using ToDoApp.Application.Users.DTO;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Users;

public interface IUserServices
{
    public Task<IEnumerable<UserDTO>> GetAllUsers();

}
