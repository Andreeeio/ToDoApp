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

}
