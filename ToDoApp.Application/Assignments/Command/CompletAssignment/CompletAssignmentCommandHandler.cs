using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Assignments.Command.AddAssignment;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Assignments.Command.CompletAssignment;

public class CompletAssignmentCommandHandler(ILogger<AddAssignmentCommandHandler> logger,
    IAssignmentRepository assignmentRepository,
    IUserContext userContext,
    IAssignmentAuthorizationService assignmentAuthorizationService) : IRequestHandler<CompletAssignmentCommand>
{
    private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly IUserContext _userContext = userContext;
    private readonly IAssignmentAuthorizationService _assignmentAuthorizationService = assignmentAuthorizationService;

    public async Task Handle(CompletAssignmentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Completing an assignment");

        var assignment = await _assignmentRepository.GetAssignmentAsync(request.Id);

        if (assignment == null)
            throw new NotFoundException(nameof(Assignment), request.Id.ToString());

        if (assignment.Completed == true)
            throw new InvalidOperationException("Assignment allready completed");

        if (!_assignmentAuthorizationService.Authorize(ResourceOperation.Update, assignment))
            throw new UnauthorizedExeption($"Current user dont have an assignment with id {request.Id}");

        assignment.Completed = true;
        await _assignmentRepository.SaveAssignmentAsync();
    }
}
