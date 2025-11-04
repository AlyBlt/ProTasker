using AutoMapper;
using ProTasker.Application.DTOs;
using ProTasker.Application.Models;
using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.Interfaces.Services
{
    public interface IProjectTaskService
    {
        Task<IEnumerable<ProjectTask>> GetAllAsync();
        Task<ProjectTask?> GetByIdAsync(Guid id);
        Task AddAsync(ProjectTask task);
        Task UpdateAsync(ProjectTask task);
        Task<bool> DeleteAsync(Guid id);
        Task<ApplicationUser?> GetUserByIdAsync(Guid userId);


    }
}
