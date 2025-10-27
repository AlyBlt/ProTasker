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
    public class TaskHistoryService : ITaskHistoryService
    {
        private readonly ITaskHistoryRepository _repository;

        public TaskHistoryService(ITaskHistoryRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TaskHistory>> GetAllAsync() => _repository.GetAllAsync();

        public Task<TaskHistory?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task AddAsync(TaskHistory history) => _repository.AddAsync(history);

        public Task UpdateAsync(TaskHistory history) => _repository.UpdateAsync(history);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
