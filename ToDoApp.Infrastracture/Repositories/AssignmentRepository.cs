using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Repositories;
using ToDoApp.Infrastracture.Presistance;

namespace ToDoApp.Infrastracture.Repositories;

public class AssignmentRepository(ToDoAppDbContext dbContext) : IAssignmentRepository
{
    public async Task<IEnumerable<Assignment>> GetAssignmentsAsync()
    {
        var assignment = await dbContext.Assignments.ToListAsync();
        return assignment;
    }

    public async Task AddAssignment(Assignment assignment)
    {
        dbContext.Assignments.Add(assignment);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Assignment>> GetAssignmentForUser(int id)
    {
        var assignment = await dbContext.Assignments.Where(x => x.UserId == id).ToListAsync();
        return assignment;
    }

    public async Task<Assignment?> GetAssignmentAsync(int id)
    {
        var assignment = await dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
        return assignment;
    }

    public async Task DeleteAssignmentAsync(Assignment assignment)
    {
        dbContext.Assignments.Remove(assignment);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveAssignmentAsync() 
        => await dbContext.SaveChangesAsync();
}