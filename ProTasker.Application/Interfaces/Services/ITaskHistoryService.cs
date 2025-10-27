using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Application.DTOs;
using AutoMapper;

namespace ProTasker.Application.Interfaces.Services
{
    public interface ITaskHistoryService
    {
        Task<IEnumerable<TaskHistory>> GetAllAsync();
        Task<TaskHistory?> GetByIdAsync(Guid id);
        Task AddAsync(TaskHistory history);
        Task UpdateAsync(TaskHistory history);
        Task<bool> DeleteAsync(Guid id);
    }
}
