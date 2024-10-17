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
    public async Task<IActionResult> GetAll()
    {
        var user = await _mediator.Send(new GetAllUsersQuery());
        return Ok(user);
    }

    [HttpGet("{id}")]   
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user == null)
            return BadRequest();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] AddUserCommand command)
    {
        if(await _mediator.Send(command) == 0)
        {
            return BadRequest(); 
        }
        return Created();
    }
    [HttpPatch("confirmemail")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
    {
        await _mediator.Send(new ConfirmEmailCommand(command.Token, command.Email));
        return NoContent();
    }

}
