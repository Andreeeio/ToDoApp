using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Users.Command.GeneratingNewToken;
using ToDoApp.Domain.Constants;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users.Command.ChangePassword;

public class ChangePasswordCommandHandler(ILogger<ChangePasswordCommandHandler> logger,
    IUserContext userContext,
    IUserAuthorizationServie userAuthorizeServie,
    IUserRepositories userRepositories) : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IUserAuthorizationServie _userAuthorizeServie = userAuthorizeServie;
    private readonly IUserRepositories _userRepositories = userRepositories;
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();

        if (!_userAuthorizeServie.Authorize(ResourceOperation.Update))
            throw new UnauthorizedExeption("Not found an user");

        logger.LogInformation($"Changing passowrd for a user with id {currentUser.Id}");

        var user = await _userRepositories.GetUserById(currentUser.Id)
            ?? throw new NotFoundException(nameof(User), currentUser.Id.ToString());


        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.oldPassword));

        if (!computeHash.SequenceEqual(user.PasswordHash))
        {
            throw new UnauthorizedExeption("Invalid login or password");
        }

        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.newPassword));
        user.PasswordSalt = hmac.Key;
        user.ResetTokenExpiration = null;
        user.ResetToken = null;

        await _userRepositories.SaveChanges();

    }
}
