using AutoMapper;
using ProTasker.Application.DTOs;
using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace ProTasker.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDTO>()
                .ReverseMap();

            // Team
            CreateMap<Team, TeamDTO>()
                .ReverseMap();

            // ProjectTask
            CreateMap<ProjectTask, ProjectTaskDTO>()
                .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedUser.FirstName + " " + src.AssignedUser.LastName))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name))
                .ReverseMap();

            // TaskHistory
            CreateMap<TaskHistory, TaskHistoryDTO>()
                .ForMember(dest => dest.PerformedByUserName, opt => opt.MapFrom(src => src.PerformedByUser.FirstName + " " + src.PerformedByUser.LastName))
                .ReverseMap();
        }
    }
}
    

