using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Entities;


namespace ProTasker.Application.Interfaces
{
    public interface IProjectTaskRepository
    {
        Task<List<ProjectTask>> GetAllAsync();
        Task<ProjectTask?> GetByIdAsync(Guid id);
        Task AddAsync(ProjectTask task);
        Task UpdateAsync(ProjectTask task);
        Task DeleteAsync(Guid id);
    }
}
