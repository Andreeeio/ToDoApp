using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Assignments.Command.AddAssignment;
using ToDoApp.Application.Assignments.DTO;
using ToDoApp.Application.Assignments.Query.GetAllAssignment;

namespace ToDoApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("task")]
public class AssignmentController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AssignmentDTO>>> GetAllAssignment()
    {
        var assignment = await _mediator.Send(new GetAllAssignmentsQuery());
        return Ok(assignment);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddAssignment(AddAssignmentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
