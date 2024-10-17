using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Constants;
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

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }

    public async Task<int> CreateUser(User user)
    {
        var role = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name == UserRoles.User);
        user.Roles = [role!];
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<bool> IfUserExist(string email, string phone)
    {
        if(await dbContext.Users.AnyAsync(x => x.Email == email) || await dbContext.Users.AnyAsync(x => x.Email == phone))
        {
            return true;
        }
        return false;
    }

    public async Task SaveChanges() 
        => await dbContext.SaveChangesAsync();


}
