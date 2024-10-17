using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Repositories;

public interface IUserRepositories
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByEmail(string email);

    Task<User?> GetUserById(int id);
    Task<int> CreateUser(User user);
    Task<bool> IfUserExist(string email, string phone);
    Task SaveChanges();

}
