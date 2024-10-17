using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users.Command.ConfirEmail;

public class ConfirmEmailCommandHandler(ILogger<ConfirmEmailCommandHandler> logger,
    IUserRepositories userRepositories) : IRequestHandler<ConfirmEmailCommand>
{
    private IUserRepositories _userRepositories = userRepositories;
    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Confirming user's email");
        var user = await _userRepositories.GetUserByEmail(request.Email)
            ?? throw new NotFoundException(nameof(User), request.Email);

        if (user.ConfirmationToken == null)
        {
            throw new InvalidOperationException("Dont have confirmation token") ;
        }
        if(user.ConfirmationTokenExpiration < DateTime.UtcNow)
        {
            user.ConfirmationToken = null;
            user.ConfirmationTokenExpiration = null;
            await _userRepositories.SaveChanges();
            throw new InvalidOperationException("Your token expired");
        }

        if(user.ConfirmationToken != request.Token)
        {
            throw new InvalidOperationException("Invalid token");
        }
        user.ConfirmationToken = null;
        user.ConfirmationTokenExpiration = null;
        user.IsEmailConfirmed = true;
        await _userRepositories.SaveChanges();
        logger.LogInformation($"{user.ConfirmationToken} {user.IsEmailConfirmed}");
    }
}
