using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Repositories;
using ToDoApp.Infrastracture.Presistance;
using ToDoApp.Infrastracture.Repositories;
using Xunit;

namespace ToDoApp.Infrastracture.Test.Repositories;

public class AssignmentRepositoryTests
{
    private readonly DbContextOptions<ToDoAppDbContext> _dbContext;
    private readonly ToDoAppDbContext _context;
    private readonly IAssignmentRepository _assignmentRepository;

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

    private readonly Assignment _assignment = new Assignment()
    {
        Name = "testAs",
        Description = "tested",
        Completed = false,
        Created = DateTime.UtcNow,
        Expired = DateTime.UtcNow.AddDays(1),
        UserId = 1
    };

    private readonly Assignment _assignment2 = new Assignment()
    {
        Name = "testAs2",
        Description = "tested2",
        Completed = true,
        Created = DateTime.UtcNow,
        Expired = DateTime.UtcNow.AddDays(1),
        UserId = 1
    };

    public AssignmentRepositoryTests()
    {
        _dbContext = new DbContextOptionsBuilder<ToDoAppDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;
        _context = new ToDoAppDbContext(_dbContext);
        _assignmentRepository = new AssignmentRepository(_context);
    }

    [Fact()]
    public async Task Create()
    {
        //arrange

        //act
        await _assignmentRepository.AddAssignment(_assignment);
        var assignment = await _assignmentRepository.GetAssignmentByIdAsync(_assignment.Id);

        //assert
        Assert.NotNull(assignment);
        Assert.Equal(assignment.Name, _assignment.Name);
    }

    [Fact()]
    public async Task GetAllTask()
    {
        //arrange

        //act
        await _assignmentRepository.AddAssignment(_assignment);
        await _assignmentRepository.AddAssignment(_assignment2);
        var assignments = await _assignmentRepository.GetAssignmentsAsync();

        //assert
        Assert.NotNull(assignments);
    }

    [Fact()]
    public async Task GetAssignmetForUser()
    {
        //arrange
        _assignment.UserId = 2;
        _assignment2.UserId = 2;

        //act
        await _assignmentRepository.AddAssignment(_assignment);
        await _assignmentRepository.AddAssignment(_assignment2);
        var assignments = await _assignmentRepository.GetAssignmentForUser(2);

        //assert
        Assert.NotNull(assignments);
        Assert.Equal(assignments.Count(),2);

    }

    [Fact()]
    public async Task GetAssignmetById()
    {
        //arrange

        //act
        await _assignmentRepository.AddAssignment(_assignment);
        var assignment = await _assignmentRepository.GetAssignmentByIdAsync(1);

        //assert
        Assert.NotNull(assignment);
        Assert.Equal(1, assignment.Id);
    }

    [Fact()]
    public async Task Delete()
    {
        //arrange

        //act
        await _assignmentRepository.AddAssignment(_assignment);
        await _assignmentRepository.DeleteAssignmentAsync(_assignment);
        var assignment = await _assignmentRepository.GetAssignmentByIdAsync(_assignment.Id);

        //assert
        Assert.Null(assignment);
    }

}
