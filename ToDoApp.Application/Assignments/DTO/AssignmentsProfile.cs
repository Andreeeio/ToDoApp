using AutoMapper;
using ToDoApp.Application.Assignments.Command.AddAssignment;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Assignments.DTO;

public class AssignmentsProfile : Profile
{
    public AssignmentsProfile()
    {
        CreateMap<Assignment,AssignmentDTO>();
        CreateMap<AddAssignmentCommand, Assignment>();

    }
}
