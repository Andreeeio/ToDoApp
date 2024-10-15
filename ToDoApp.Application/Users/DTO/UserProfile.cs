using AutoMapper;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Users.DTO;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>();
    }
}
