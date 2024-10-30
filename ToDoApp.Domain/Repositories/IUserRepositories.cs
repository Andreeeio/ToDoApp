using System.Linq.Expressions;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Repositories;

public interface IUserRepositories
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByEmail(string email, params Expression<Func<User, object>>[] includePredicates);
    Task<User?> GetUserById(int id);
    Task<int> CreateUser(User user);
    Task<bool> IfUserExist(string email, string phone);
    Task DeleteUser(User user);
    Task SaveChanges();

}
