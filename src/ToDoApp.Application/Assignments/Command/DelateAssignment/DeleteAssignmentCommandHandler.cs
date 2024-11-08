using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Assignments.Command.DeleteAssignment;

public class DeleteAssignmentCommandHandler(ILogger<DeleteAssignmentCommandHandler> logger,
    IUserContext userContext,
    IAssignmentAuthorizationService assignmentAuthorizationService,
    IAssignmentRepository assignmentRepository) : IRequestHandler<DeleteAssignmentCommand>
{
    private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
    private readonly IUserContext _userContext = userContext;
    private readonly IAssignmentAuthorizationService _assignmentAuthorizationService = assignmentAuthorizationService;

    public async Task Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting and assignment with id: {request.Id}");
        var assignment = await _assignmentRepository.GetAssignmentAsync(request.Id);

        if (assignment == null) 
            throw new NotFoundException(nameof(Assignment),request.Id.ToString());

        if (!_assignmentAuthorizationService.Authorize(ResourceOperation.Delete, assignment))
            throw new UnauthorizedExeption($"Current user dont have an assignment with id {request.Id}");

        await _assignmentRepository.DeleteAssignmentAsync(assignment);
    }
}
