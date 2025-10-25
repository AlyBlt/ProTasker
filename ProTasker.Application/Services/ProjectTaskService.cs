using ProTasker.Application.Interfaces;
using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Application.DTOs;
using AutoMapper;

namespace ProTasker.Application.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _repository;
   
        public ProjectTaskService(IProjectTaskRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ProjectTask>> GetAllTasksAsync() => _repository.GetAllAsync();
        public Task<ProjectTask?> GetTaskByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public Task AddTaskAsync(ProjectTask task) => _repository.AddAsync(task);
        public Task UpdateTaskAsync(ProjectTask task) => _repository.UpdateAsync(task);
        public Task DeleteTaskAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
