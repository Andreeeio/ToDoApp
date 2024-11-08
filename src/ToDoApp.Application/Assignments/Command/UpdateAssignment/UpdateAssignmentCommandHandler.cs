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

public class UpdateAssignmentCommandHandler(ILogger<UpdateAssignmentCommandHandler> logger,
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
        var assignment = await _assignmentRepository.GetAssignmentAsync(request.Id)
            ?? throw new NotFoundException(nameof(Assignment),request.Id.ToString());

        var currentUser = _userContext.GetCurrentUser()
            ?? throw new UnauthorizedExeption("Unauthorized user");

        if (!_assignmentAuthorizationService.Authorize(ResourceOperation.Update, assignment))
            throw new UnauthorizedExeption("Unauthorized user");

        int i = 0;
        if(request.Name !=  null)
        {
            i++;
            assignment.Name = request.Name;
        }
        if (request.Description != null)
        {
            i++;
            assignment.Description = request.Description;
        }
        if (request.Completed != null)
        {
            i++;
            request.Completed = assignment.Completed;
        }
        if (request.Expired != null)
        { 
            i++;
            request.Expired = assignment.Expired;
        }
        if (i == 0)
            throw new InvalidOperationException();

        await _assignmentRepository.SaveAssignmentAsync();
        
    }
}
