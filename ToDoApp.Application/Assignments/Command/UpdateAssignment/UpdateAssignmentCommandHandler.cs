using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Assignments.Command.DeleteAssignment;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Assignments.Command.UpdateAssignment;

public class UpdateAssignmentCommandHandler(ILogger<DelateAssignmentCommandHandler> logger,
    IUserContext userContext,
    IAssignmentAuthorizationService assignmentAuthorizationService,
    IAssignmentRepository assignmentRepository) : IRequestHandler<UpdateAssignmentCommand>
{
    private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly IUserContext _userContext = userContext;
    private readonly IAssignmentAuthorizationService _assignmentAuthorizationService = assignmentAuthorizationService;
    public async Task Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updateing an assignment");
        var assignment = await _assignmentRepository.GetAssignmentAsync(request.id)
            ?? throw new NotFoundException(nameof(Assignment),request.id.ToString());

        var currentUser = _userContext.GetCurrentUser()
            ?? throw new UnauthorizedExeption("Unauthorized user");

        if (!_assignmentAuthorizationService.Authorize(ResourceOperation.Update, assignment))
            throw new UnauthorizedExeption("Unauthorized user");

        int i = 0;
        if(request.name !=  null)
        {
            i++;
            assignment.Name = request.name;
        }
        if (request.description != null)
        {
            i++;
            assignment.Description = request.description;
        }
        if (request.completed != null)
        {
            i++;
            request.completed = assignment.Completed;
        }
        if (request.expired != null)
        { 
            i++;
            request.expired = assignment.Expired;
        }
        if (i == 0)
            throw new InvalidOperationException();

        await _assignmentRepository.SaveAssignmentAsync();
        
    }
}
