using AutoMapper;
using ProTasker.Application.DTOs;
using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ProTasker.Application.Helpers;



namespace ProTasker.Application.Mapping

{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.TeamName,
                    opt => opt.MapFrom(src =>
                        src.Team != null ? StringHelpers.CapitalizeWords(src.Team.Name) : string.Empty))
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => StringHelpers.CapitalizeWords(src.UserName)))
                .ReverseMap();

            // Team
            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => StringHelpers.CapitalizeWords(src.Name)))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => StringHelpers.Capitalize(src.Description)))
                .ForMember(dest => dest.LeaderName,
                    opt => opt.MapFrom(src =>
                        src.Leader != null ? StringHelpers.CapitalizeWords(src.Leader.UserName) : string.Empty))
                .ReverseMap();

            // ProjectTask
            CreateMap<ProjectTask, ProjectTaskDTO>()
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => StringHelpers.CapitalizeWords(src.Title)))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => StringHelpers.Capitalize(src.Description)))
                .ForMember(dest => dest.AssignedUserName,
                    opt => opt.MapFrom(src =>
                        src.AssignedUser != null ? StringHelpers.CapitalizeWords(src.AssignedUser.UserName) : string.Empty))
                .ForMember(dest => dest.TeamName,
                    opt => opt.MapFrom(src => StringHelpers.CapitalizeWords(src.Team.Name)))
                .ReverseMap();


            // TaskHistory
            CreateMap<TaskHistory, TaskHistoryDTO>()
                .ForMember(dest => dest.PerformedByUserName,
                    opt => opt.MapFrom(src =>
                        src.PerformedByUser != null ? StringHelpers.CapitalizeWords(src.PerformedByUser.UserName) : string.Empty))
                .ReverseMap();
        }
    }
}
    
