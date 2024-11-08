using MediatR;
using ToDoApp.Application.Assignments.DTO;

namespace ToDoApp.Application.Assignments.Query.GetUserAssignments;

public class GetUserAssignmentsQuery() : IRequest<IEnumerable<AssignmentDTO>>
{
}
