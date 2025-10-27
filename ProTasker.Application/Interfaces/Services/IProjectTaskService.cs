using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Entities;
using ProTasker.Application.DTOs;
using AutoMapper;

namespace ProTasker.Application.Interfaces.Services
{
    public interface IProjectTaskService
    {
        Task<IEnumerable<ProjectTask>> GetAllAsync();
        Task<ProjectTask?> GetByIdAsync(Guid id);
        Task AddAsync(ProjectTask task);
        Task UpdateAsync(ProjectTask task);
        Task<bool> DeleteAsync(Guid id);
    }
}
