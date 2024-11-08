using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Repositories;

public interface IAssignmentRepository
{
    Task<Assignment?> GetAssignmentAsync(int id);
    Task DeleteAssignmentAsync(Assignment assignment);
    Task<IEnumerable<Assignment>> GetAssignmentsAsync();
    Task AddAssignment(Assignment assignment);
    Task<IEnumerable<Assignment>> GetAssignmentForUser(int id);
    Task SaveAssignmentAsync();
}