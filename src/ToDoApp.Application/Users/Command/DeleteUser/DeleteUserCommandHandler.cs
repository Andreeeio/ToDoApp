using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Users.Command.GeneratingNewToken;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users.Command.DeleteUser;

public class DeleteUserCommandHandler(ILogger<GeneratingNewTokenCommand> logger,
    IUserContext userContext,
    IUserAuthorizationServie userAuthorizeServie,
    IUserRepositories userRepositories) : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IUserAuthorizationServie _userAuthorizeServie = userAuthorizeServie;
    private readonly IUserRepositories _userRepositories = userRepositories;
    
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        if (!_userAuthorizeServie.Authorize(ResourceOperation.Delete))
        {
            throw new UnauthorizedExeption("Not found an user");
        }
        logger.LogInformation($"Deleting an user with id {currentUser.id}");

        var user = await _userRepositories.GetUserById(currentUser.id) 
            ?? throw new NotFoundException(nameof(User), currentUser.id.ToString());

        await _userRepositories.DeleteUser(user);
    }
}
