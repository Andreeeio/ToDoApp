﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using ToDoApp.Application.Users;
using ToDoApp.Application.Users.Command.AddUserCommand;
using ToDoApp.Application.Users.Command.ChangePassword;
using ToDoApp.Application.Users.Command.ChangePasswordWithToken;
using ToDoApp.Application.Users.Command.ConfirEmail;
using ToDoApp.Application.Users.Command.DeleteUser;
using ToDoApp.Application.Users.Command.GenerateResetToken;
using ToDoApp.Application.Users.Command.GeneratingNewToken;
using ToDoApp.Application.Users.DTO;
using ToDoApp.Application.Users.Query.GetAllUsers;
using ToDoApp.Application.Users.Query.GetUserById;
using ToDoApp.Application.Users.Query.LoginUser;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Api.Controllers;

[ApiController]
[Route("users")]
public class UserControler(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll()
    {
        var user = await _mediator.Send(new GetAllUsersQuery());
        return Ok(user);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(user);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateUser([FromBody] AddUserCommand command)
    {
        await _mediator.Send(command);
        return Created();
    }
    [HttpPatch("confirmemail")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ConfirmEmail(ConfirmEmailCommand command)
    {
        await _mediator.Send(new ConfirmEmailCommand(command.Token, command.Email));
        return NoContent();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JwtToken>> Login(LoginUserQuery query)
    {
        var token = await _mediator.Send(query);
        return Ok(token);
    }

    [HttpPatch("confirmationtoken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetNewToken()
    {
        await _mediator.Send(new GeneratingNewTokenCommand());
        return Ok();
    }

    [HttpPatch("resettoken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetNewResetToken(GenerateResetTokenCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteUser()
    {
        await _mediator.Send(new DeleteUserCommand());
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangePassword(ChangePasswordCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangePasswordWithToken(ChangePasswordWithTokenCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}
