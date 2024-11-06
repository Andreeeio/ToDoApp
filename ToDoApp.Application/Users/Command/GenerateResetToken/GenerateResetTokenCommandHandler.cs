using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Users.Command.GeneratingNewToken;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users.Command.GenerateResetToken;

public class GenerateResetTokenCommandHandler(ILogger<GenerateResetTokenCommandHandler> logger,
    IUserRepositories userRepositories) : IRequestHandler<GenerateResetTokenCommand>
{
    private readonly IUserRepositories _userRepositories = userRepositories;

    public async Task Handle(GenerateResetTokenCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating reset token for user");

        var user = await _userRepositories.GetUserByEmail(request.email)
            ?? throw new NotFoundException(nameof(User), request.email);

        user.ResetTokenExpiration = DateTime.UtcNow.AddMinutes(30);
        user.ResetToken = Guid.NewGuid().ToString();

        await _userRepositories.SaveChanges();
    }
}