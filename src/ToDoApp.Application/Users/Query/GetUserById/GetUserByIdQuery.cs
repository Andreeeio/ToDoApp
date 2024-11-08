using MediatR;
using ToDoApp.Application.Users.DTO;

namespace ToDoApp.Application.Users.Query.GetUserById;

public class GetUserByIdQuery : IRequest<UserDTO>
{
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}