using MediatR;
using ToDoApp.Application.Assignments.DTO;

namespace ToDoApp.Application.Assignments.Query.GetAllAssignment;

public class GetAllAssignmentsQuery : IRequest<IEnumerable<AssignmentDTO>>
{
}
