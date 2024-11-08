using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users.Command.GeneratingNewToken;

public class GeneratingNewTokenCommandHandler(ILogger<GeneratingNewTokenCommand> logger,
    IUserContext userContext,
    IUserAuthorizationServie userAuthorizeServie,
    IUserRepositories userRepositories) : IRequestHandler<GeneratingNewTokenCommand>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IUserAuthorizationServie _userAuthorizeServie = userAuthorizeServie;
    private readonly IUserRepositories _userRepositories = userRepositories;

    public async Task Handle(GeneratingNewTokenCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();

        if (!_userAuthorizeServie.Authorize(ResourceOperation.Update))
            throw new UnauthorizedExeption("Not found an user");

        logger.LogInformation($"Generating a new token for user with id {currentUser.id}");

        var user = await _userRepositories.GetUserById(currentUser.id)
            ?? throw new NotFoundException(nameof(User),currentUser.id.ToString());

        user.ConfirmationTokenExpiration = DateTime.UtcNow.AddDays(1);
        user.ConfirmationToken = Guid.NewGuid().ToString();

        await _userRepositories.SaveChanges();
    }
}