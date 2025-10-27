using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Application.DTOs;
using AutoMapper;
using ProTasker.Application.Interfaces.Repositories;
using ProTasker.Application.Interfaces.Services;

namespace ProTasker.Application.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _repository;
   
        public ProjectTaskService(IProjectTaskRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ProjectTask>> GetAllAsync() => _repository.GetAllAsync();

        public Task<ProjectTask?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task AddAsync(ProjectTask task) => _repository.AddAsync(task);

        public Task UpdateAsync(ProjectTask task) => _repository.UpdateAsync(task);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
