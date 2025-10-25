using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Entities;
using ProTasker.Application.DTOs;
using AutoMapper;

namespace ProTasker.Application.Interfaces
{
    public interface IProjectTaskService
    {
        Task<List<ProjectTask>> GetAllTasksAsync();
        Task<ProjectTask?> GetTaskByIdAsync(Guid id);
        Task AddTaskAsync(ProjectTask task);
        Task UpdateTaskAsync(ProjectTask task);
        Task DeleteTaskAsync(Guid id);
    }
}
