using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Assignments.Command.AddAssignment;
using ToDoApp.Application.Assignments.Command.CompletAssignment;
using ToDoApp.Application.Assignments.Command.DeleteAssignment;
using ToDoApp.Application.Assignments.Command.UpdateAssignment;
using ToDoApp.Application.Assignments.DTO;
using ToDoApp.Application.Assignments.Query.GetAllAssignment;
using ToDoApp.Application.Assignments.Query.GetUserAssignments;

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

    [HttpGet("yourtasks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<AssignmentDTO>>> GetUserAssignment()
    {
        var assignments = await _mediator.Send(new GetUserAssignmentsQuery());
        return Ok(assignments);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> AddAssignment(AddAssignmentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DelateAssignment([FromRoute]int id)
    {
        await _mediator.Send(new DelateAssignmentCommand(id));
        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CompletedAssignment([FromRoute]int id)
    {
        await _mediator.Send(new CompletAssignmentCommand(id));
        return Ok();
    }

    [HttpPatch("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAssignment([FromRoute] int id, UpdateAssignmentCommand command)
    {
        command.id = id;
        await _mediator.Send(command);
        return Ok();
    }
}
