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
    public class TaskHistoryService : ITaskHistoryService
    {
        private readonly ITaskHistoryRepository _repository;

        public TaskHistoryService(ITaskHistoryRepository repository)
        {
            _repository = repository;
        }

        public Task<List<TaskHistory>> GetAllHistoriesAsync() => _repository.GetAllAsync();
        public Task<TaskHistory?> GetHistoryByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public Task AddHistoryAsync(TaskHistory history) => _repository.AddAsync(history);
        public Task UpdateHistoryAsync(TaskHistory history) => _repository.UpdateAsync(history);
        public Task DeleteHistoryAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
