using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Repositories;

public interface IAssignmentRepository
{
    Task<IEnumerable<Assignment>> GetAssignmentsAsync();
    Task AddAssignment(Assignment assignment);
}
