using AutoMapper;
using ProTasker.Application.DTOs;
using ProTasker.Application.Helpers;
using ProTasker.Application.Models;
using ProTasker.Domain.Entities;

namespace ProTasker.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ---------------- APPLICATIONUSER -- USER ----------------
            CreateMap<ApplicationUser, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.TeamId)) // sadece ID
                .ReverseMap()
                .ForMember(dest => dest.Tasks, opt => opt.Ignore()) // navigation ignore
                .ForMember(dest => dest.TaskHistories, opt => opt.Ignore());

            // ---------------- APPLICATIONUSER -- USERDTO ----------------
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // eksikse ekle
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.TeamId))
                .ReverseMap()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // ---------------- TEAM -- TEAMDTO ----------------
            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.LeaderId, opt => opt.MapFrom(src => src.LeaderId)) // ID üzerinden // navigation kalabilir
                .ReverseMap()
                .ForMember(dest => dest.Tasks, opt => opt.Ignore()); // Task navigations ignore

            // ---------------- PROJECTTASK -- PROJECTTASKDTO ----------------
            CreateMap<ProjectTask, ProjectTaskDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.TeamId))
                .ForMember(dest => dest.AssignedUserId, opt => opt.MapFrom(src => src.AssignedUserId))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team != null 
                        ? StringHelpers.CapitalizeWords(src.Team.Name) 
                        : string.Empty));

            CreateMap<ProjectTaskDTO, ProjectTask>()
                .ForMember(dest => dest.Team, opt => opt.Ignore()) // navigation ignore
                .ForMember(dest => dest.Histories, opt => opt.Ignore());

            // ---------------- TASKHISTORY -- TASKHISTORYDTO ----------------
            CreateMap<TaskHistory, TaskHistoryDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId)) // ID üzerinden
                .ForMember(dest => dest.PerformedByUserId, opt => opt.MapFrom(src => src.PerformedByUserId)) // ID üzerinden
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<TaskHistoryDTO, TaskHistory>()
                .ForMember(dest => dest.Task, opt => opt.Ignore());
              
        }
    }
}