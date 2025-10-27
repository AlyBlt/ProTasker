using ProTasker.Application.DTOs;
using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.Interfaces.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllAsync();
        Task<Team?> GetByIdAsync(Guid id);
        Task AddAsync(Team team);
        Task UpdateAsync(Team team);
        Task<bool> DeleteAsync(Guid id);
    }
}
