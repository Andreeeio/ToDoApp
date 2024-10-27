using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Assignments.Command.AddAssignment;

public class AddAssignmentCommandHandler(ILogger<AddAssignmentCommandHandler> logger,
    IMapper mapper,
    IAssignmentRepository assignmentRepository,
    IUserContext userContext,
    IAssignmentAuthorizationService assignmentAuthorizationService) : IRequestHandler<AddAssignmentCommand>
{
    private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUserContext _userContext = userContext;
    private readonly IAssignmentAuthorizationService _assignmentAuthorizationService = assignmentAuthorizationService;
    public async Task Handle(AddAssignmentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding an assignment");
        var user = _userContext.GetCurrentUser();
        var assignment = _mapper.Map<Assignment>(request);
        if (!_assignmentAuthorizationService.Authorize(ResourceOperation.Create, assignment))
            throw new UnauthorizedAccessException("Not authorized user");

        assignment.Created = DateTime.UtcNow;
        assignment.User_Id = user.Id;
        assignment.Completed = false;

        await _assignmentRepository.AddAssignment(assignment);
    }
}
