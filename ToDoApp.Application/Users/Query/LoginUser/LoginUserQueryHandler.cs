using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Users.DTO;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users.Query.LoginUser;

public class LoginUserQueryHandler(ILogger<LoginUserQueryHandler> logger,
    IUserRepositories userRepositories,
    ITokenService tokenService) : IRequestHandler<LoginUserQuery,JwtToken>
{
    private readonly IUserRepositories _userRepositories = userRepositories;
    private readonly ITokenService _tokenService = tokenService;
    public async Task<JwtToken> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("User tring to log in");

        var user = await _userRepositories.GetUserByEmail(request.login, u => u.Roles)
            ?? throw new NotFoundException(nameof(User), request.login);

        using var hmac =  new HMACSHA512(user.PasswordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.password));

        if (!computeHash.SequenceEqual(user.PasswordHash))
        {
            throw new UnauthorizedExeption("Invalid login or password");
        }

        var jwtToken = new JwtToken(_tokenService.GetToken(user));
        return jwtToken;
    }
}
