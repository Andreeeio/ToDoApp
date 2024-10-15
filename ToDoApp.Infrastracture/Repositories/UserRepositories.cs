using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Repositories;
using ToDoApp.Infrastracture.Presistance;

namespace ToDoApp.Infrastracture.Repositories;

public class UserRepositories(ToDoAppDbContext dbContext) : IUserRepositories
{
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await dbContext.Users.ToListAsync();
        return users;
    }

    public async Task<User?> GetUserById(int id)
    {
        var user = await dbContext.Users.FindAsync(id);
        return user;
    }
}
