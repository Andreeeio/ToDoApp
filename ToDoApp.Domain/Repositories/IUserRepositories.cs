using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Repositories;

public interface IUserRepositories
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserById(int id);
}
