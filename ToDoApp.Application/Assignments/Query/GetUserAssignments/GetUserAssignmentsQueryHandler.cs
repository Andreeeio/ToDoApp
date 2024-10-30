using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Assignments.DTO;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Assignments.Query.GetUserAssignments;

public class GetUserAssignmentsQueryHandler(ILogger<GetUserAssignmentsQueryHandler> logger,
    IUserContext userContext,
    IAssignmentRepository assignmentRepository,
    IMapper mapper,
    IAssignmentAuthorizationService assignmentAuthorizationService) : IRequestHandler<GetUserAssignmentsQuery, IEnumerable<AssignmentDTO>>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IAssignmentAuthorizationService _assignmentAuthorizationService = assignmentAuthorizationService;
    public async Task<IEnumerable<AssignmentDTO>> Handle(GetUserAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        var assignments = await _assignmentRepository.GetAssignmentForUser(currentUser.Id);
        logger.LogInformation($"User with id {currentUser.Id} adding assignment");

        if (!_assignmentAuthorizationService.Authorize(ResourceOperation.Read, new Assignment()))
        {
            throw new UnauthorizedAccessException();
        }

        var assignmentsDTO = _mapper.Map<IEnumerable<AssignmentDTO>>(assignments);

        return assignmentsDTO;

    }
}
