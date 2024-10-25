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
    IAssignmentAuthorization assignmentAuthorization) : IRequestHandler<AddAssignmentCommand>
{
    private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUserContext _userContext = userContext;
    private readonly IAssignmentAuthorization _assignmentAuthorization = assignmentAuthorization;
    public async Task Handle(AddAssignmentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding an assignment");
        var user = _userContext.GetCurrentUser();
        if (!_assignmentAuthorization.Authorize(ResourceOperation.Create))
            throw new UnauthorizedAccessException("Not authorized user");

        var assignment = _mapper.Map<Assignment>(request);
        assignment.User_Id = user.Id;
        assignment.Completed = false;

        await _assignmentRepository.AddAssignment(assignment);
    }
}
