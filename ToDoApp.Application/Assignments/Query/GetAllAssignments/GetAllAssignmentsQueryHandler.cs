using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Assignments.DTO;
using ToDoApp.Application.Assignments.Query.GetAllAssignment;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Assignments.Query.GetAllAssignments;

public class GetAllAssignmentsQueryHandler(ILogger<GetAllAssignmentsQueryHandler> logger,
    IMapper mapper,
    IAssignmentRepository assignmentRepository) : IRequestHandler<GetAllAssignmentsQuery, IEnumerable<AssignmentDTO>>
{
    private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<AssignmentDTO>> Handle(GetAllAssignmentsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all assignmments");
        var assignmments = await _assignmentRepository.GetAssignmentsAsync();
        var assignmmentsDTO = _mapper.Map<IEnumerable<AssignmentDTO>>(assignmments);
        return assignmmentsDTO;
    }
}
