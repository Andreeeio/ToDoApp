using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Repositories;
using ToDoApp.Infrastracture.Presistance;
using ToDoApp.Infrastracture.Repositories;
using Xunit;

namespace ToDoApp.Infrastracture.Test.Repositories;

public class UserRepositoriesTests
{
    private readonly DbContextOptions<ToDoAppDbContext> _dbContext;
    private readonly ToDoAppDbContext _context;
    private readonly IUserRepositories _userRepositories;

    private readonly User _user = new User()
    {
        FirstName = "Test",
        LastName = "Tester",
        Email = "test@test.com",
        Phone = "123456789",
        DateOfBirth = new DateOnly(2000, 1, 1),
        PasswordHash = [],
        PasswordSalt = []

    };
    private readonly User _user2 = new User()
    {
        FirstName = "Test2",
        LastName = "Tester2",
        Email = "tes2t@test.com",
        Phone = "123156789",
        DateOfBirth = new DateOnly(2000, 1, 1),
        PasswordHash = [],
        PasswordSalt = []

    };

    public UserRepositoriesTests()
    {
        _dbContext = new DbContextOptionsBuilder<ToDoAppDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;
        _context = new ToDoAppDbContext(_dbContext);
        _userRepositories = new UserRepositories(_context);
        if (!_context.Roles.Any())
        {
            _context.Roles.AddRangeAsync(new Roles
            {
                Id = 1,
                Name = "User",
            });
            _context.SaveChanges();
        }
    }

    private void dispose()
    {
        _context.Users.RemoveRange(_context.Users);
        _context.SaveChanges();
    }

    [Fact()]
    public async Task Create()
    {
        //arrange
        dispose();

        //act
        await _userRepositories.CreateUser(_user);
        var userFromDb = await _userRepositories.GetUserById(_user.Id);

        //assert
        Assert.NotNull(userFromDb);
        Assert.Equal(userFromDb.FirstName, _user.FirstName);
    }

    [Fact()]
    public async Task GetOne()
    {
        //arrange

        //act
        await _userRepositories.CreateUser(_user);
        var userFromDb = await _userRepositories.GetUserById(_user.Id);

        //assert
        Assert.NotNull(userFromDb);
    }

    [Fact()]
    public async Task GetAll()
    {
        //arrange

        //act
        await _userRepositories.CreateUser(_user2);
        var usersFromDb = await _userRepositories.GetAllUsersAsync();

        //assert
        Assert.NotNull(usersFromDb);
    }

    [Fact()]
    public async Task GetOneEmail()
    {
        //arrange

        //act
        await _userRepositories.CreateUser(_user2);
        var userFromDb = await _userRepositories.GetUserByEmail(_user2.Email);

        //assert
        Assert.NotNull(userFromDb);
        Assert.Equal(userFromDb.Email, _user2.Email);
    }

    [Fact()]
    public async Task IfExist_True()
    {
        //arrange

        //act
        await _userRepositories.CreateUser(_user);
        var ifExist = await _userRepositories.IfUserExist(_user.Email, "");

        //assert
        Assert.Equal(ifExist, true);
    }

    [Fact()]
    public async Task IfExist_False()
    {
        //arrange

        //act
        var ifExist = await _userRepositories.IfUserExist(_user.Phone, _user2.Email);

        //assert
        Assert.Equal(ifExist, false);
    }

    [Fact()]
    public async Task Delete()
    {
        //arrange

        //act
        await _userRepositories.CreateUser(_user2);
        await _userRepositories.DeleteUser(_user2);
        var ifExist = await _userRepositories.IfUserExist(_user2.Email, _user2.Phone);

        //assert
        Assert.Equal(ifExist, false);
    }


}
