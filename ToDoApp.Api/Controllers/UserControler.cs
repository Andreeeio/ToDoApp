using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Users;
using ToDoApp.Application.Users.Query.GetAllUsers;
using ToDoApp.Application.Users.Query.GetUserById;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Api.Controllers;

[ApiController]
[Route("user")]
public class UserControler(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var user = await _mediator.Send(new GetAllUsersQuery());
        return Ok(user);
    }

    [HttpGet("getById/{id}")]   
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user == null)
            return BadRequest();
        return Ok(user);
    }
}
