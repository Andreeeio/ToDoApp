using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Application.Users.DTO;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Users.Query.GetUserById;

public class GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger
    ,IUserRepositories userRepositories,
    IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDTO>
{
    private readonly IUserRepositories _userRepositories = userRepositories;
    private readonly IMapper _mapper = mapper;
    public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation(@"Get user with id {id}", request.Id);

        var user = await _userRepositories.GetUserById(request.Id);

        if (user == null)
        {
            throw new NotFoundException(nameof(User),request.Id.ToString());
        }

        var userDTO = _mapper.Map<UserDTO>(user);
        return userDTO;

    }
}
