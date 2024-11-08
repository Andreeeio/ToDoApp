using MediatR;
using ToDoApp.Application.Users.DTO;

namespace ToDoApp.Application.Users.Query.GetAllUsers;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDTO>>
{
}