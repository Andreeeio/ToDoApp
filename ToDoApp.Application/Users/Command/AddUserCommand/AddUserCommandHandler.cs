using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users.Command.AddUserCommand;

public class AddUserCommandHandler(ILogger<AddUserCommandHandler> logger,
    IUserRepositories userRepositories,
    IMapper mapper) : IRequestHandler<AddUserCommand, int>
{
    private readonly IUserRepositories _userRepositories = userRepositories;
    private readonly IMapper _mapper = mapper;
    public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        if(await _userRepositories.IfUserExist(request.Email, request.Phone))
        {
            throw new InvalidOperationException($"User with email {request.Email} already exist");
        }
        logger.LogInformation($"Creating a new user");

        var hmac = new HMACSHA512();

        var user = _mapper.Map<User>(request);

        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        user.PasswordSalt = hmac.Key;
        user.IsEmailConfirmed = false;
        user.ConfirmationTokenExpiration = DateTime.UtcNow.AddDays(1); 
        user.ConfirmationToken = Guid.NewGuid().ToString();

        return await _userRepositories.CreateUser(user);

    }
}
