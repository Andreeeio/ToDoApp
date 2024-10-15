using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Users.DTO;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users.Query.GetAllUsers;

public class GetAllUsersQueryHandler(ILogger<GetAllUsersQueryHandler> logger,
    IUserRepositories userRepositories,
    IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
{

    private readonly IUserRepositories _userRepositories = userRepositories;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all users");
        var users = await _userRepositories.GetAllUsersAsync();

        var usersDtos = mapper.Map<IEnumerable<UserDTO>>(users);
        return usersDtos;
    }
}
