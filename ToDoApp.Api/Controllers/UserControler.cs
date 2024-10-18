using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Users;
using ToDoApp.Application.Users.Command.AddUserCommand;
using ToDoApp.Application.Users.Command.ConfirEmail;
using ToDoApp.Application.Users.Query.GetAllUsers;
using ToDoApp.Application.Users.Query.GetUserById;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Api.Controllers;

[ApiController]
[Route("users")]
public class UserControler(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var user = await _mediator.Send(new GetAllUsersQuery());
        return Ok(user);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] AddUserCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }
    [HttpPatch("confirmemail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
    {
        await _mediator.Send(new ConfirmEmailCommand(command.Token, command.Email));
        return NoContent();
    }

}
