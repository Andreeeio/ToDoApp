using Bogus;
using System.Security.Cryptography;
using System.Text;
using ToDoApp.Domain.Entities;
using ToDoApp.Infrastracture.Presistance;

namespace ToDoApp.Infrastracture.Seeder;

public class ToDoAppSeeder(ToDoAppDbContext dbContext) : IToDoAppSeeder
{
    private readonly ToDoAppDbContext _dbContext = dbContext;
    private const string Locale = "pl";
    private int Counter = 5;
    public async Task Seed()
    {
        if (await _dbContext.Database.CanConnectAsync())
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                await _dbContext.SaveChangesAsync();
            }

            if (!_dbContext.Users.Any())
            {
                var users = GetUsers(Counter);

                var roles = _dbContext.Roles.ToArray();
                var assignment = GetAssignments(Counter);

                for (int i = 0; i < Counter; i++)
                {
                    assignment = GetAssignments(Counter);
                    users[i].Assignments = assignment;

                    var roleIndex = Random.Shared.Next(roles.Length);
                    users[i].Roles = [roles[roleIndex]];
                    _dbContext.Assignments.AddRange(assignment);
                }
                

                _dbContext.Users.AddRange(users);

                await _dbContext.SaveChangesAsync();


            }
        }
    }

    private List<Roles> GetRoles()
    {
        return new List<Roles>
        {
            new() {Name = "User"},
            new() {Name = "Trainer"},
            new() {Name = "Admin"}
        };
    }

    private List<User> GetUsers(int Count)
    {
        var (passwordHash, passwordSalt) = GeneratePassword("Password#123"); 

        var users = new Faker<User>(Locale)
            .RuleFor(x => x.FirstName, y => y.Name.FirstName())
            .RuleFor(x => x.LastName, y => y.Name.LastName())
            .RuleFor(x => x.Email, y => y.Internet.Email())
            .RuleFor(x => x.Phone, y => y.Phone.PhoneNumber())
            .RuleFor(x => x.DateOfBirth, y => y.Date.BetweenDateOnly(new DateOnly(1970, 1, 1), new DateOnly(2006, 12, 31)))
            .RuleFor(x => x.IsEmailConfirmed, y => y.Random.Bool())
            .RuleFor(x => x.PasswordHash, passwordHash)
            .RuleFor(x => x.PasswordSalt, passwordSalt)
            .Generate(Count);

        return users;
    }

    private List<Assignment> GetAssignments(int count)
    {
        var assignments = new Faker<Assignment>(Locale)
            .RuleFor(x => x.Name, y => y.Lorem.Word())
            .RuleFor(x => x.Description, y => y.Lorem.ToString())
            .RuleFor(x => x.Created, y => y.Date.RecentDateOnly())
            .RuleFor(x => x.Expired, y => y.Date.SoonDateOnly())
            .Generate(count);

        return assignments;
    }

    private (byte[] PasswordHash, byte[] PasswordSalt) GeneratePassword(string password)
    {
        using var hmac = new HMACSHA512();
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return (passwordHash, hmac.Key);
    }
}
