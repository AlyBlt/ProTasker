using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Application.DTOs;
using AutoMapper;

namespace ProTasker.Application.Interfaces
{
    public interface ITaskHistoryService
    {
        Task<List<TaskHistory>> GetAllHistoriesAsync();
        Task<TaskHistory?> GetHistoryByIdAsync(Guid id);
        Task AddHistoryAsync(TaskHistory history);
        Task UpdateHistoryAsync(TaskHistory history);
        Task DeleteHistoryAsync(Guid id);
    }
}
