using Microsoft.EntityFrameworkCore;
using ProTasker.Application.Interfaces;
using ProTasker.Domain.Entities;
using ProTasker.Infrastructure.Data;
using ProTasker.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Enums;

namespace ProTasker.Infrastructure.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly AppDbContext _context;

        public ProjectTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectTask>> GetAllAsync() => await _context.Tasks
            .Include(t => t.AssignedUser)
            .Include(t => t.Team)
            .Include(t => t.Histories)
            .ThenInclude(h => h.PerformedByUser)
            .ToListAsync();

        public async Task<ProjectTask?> GetByIdAsync(Guid id) => await _context.Tasks
            .Include(t => t.AssignedUser)
            .Include(t => t.Team)
            .Include(t => t.Histories)
            .ThenInclude(h => h.PerformedByUser)
            .FirstOrDefaultAsync(t => t.Id == id);

        public async Task AddAsync(ProjectTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectTask task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
