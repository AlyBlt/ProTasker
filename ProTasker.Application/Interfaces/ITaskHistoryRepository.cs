using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.Interfaces
{
    public interface ITaskHistoryRepository
    {
        Task<List<TaskHistory>> GetAllAsync();
        Task<TaskHistory?> GetByIdAsync(Guid id);
        Task AddAsync(TaskHistory history);
        Task UpdateAsync(TaskHistory history);
        Task DeleteAsync(Guid id);
    }
}
