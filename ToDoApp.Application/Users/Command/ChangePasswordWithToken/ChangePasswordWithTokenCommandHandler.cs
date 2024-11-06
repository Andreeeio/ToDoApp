using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using ToDoApp.Application.Users.Command.AddUserCommand;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users.Command.ChangePasswordWithToken;

public class ChangePasswordWithTokenCommandHandler(ILogger<ChangePasswordWithTokenCommandHandler> logger,
    IUserRepositories userRepositories) : IRequestHandler<ChangePasswordWithTokenCommand>
{
    private readonly IUserRepositories _userRepositories = userRepositories;

    public async Task Handle(ChangePasswordWithTokenCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Chaing password for user with email {request.Email}");
        
        var user = await _userRepositories.GetUserByEmail(request.Email) 
            ?? throw new NotFoundException(nameof(User), request.Email);

        if(user.ResetToken == null || user.ResetTokenExpiration < DateTime.Now)
        {
            user.ResetTokenExpiration = null;
            user.ResetToken = null;
            await _userRepositories.SaveChanges();
            
            throw new InvalidOperationException();
        }

        if (user.ResetTokenExpiration < DateTime.Now)
            throw new InvalidOperationException("Your token expired");

        if (user.ResetToken != request.Token)
            throw new InvalidOperationException("Invalid token");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        user.PasswordSalt = hmac.Key;
        user.ResetTokenExpiration = null;
        user.ResetToken = null;

        await _userRepositories.SaveChanges();
    }
}
