using Microsoft.EntityFrameworkCore;
using ProTasker.Domain.Entities;
using ProTasker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Enums;
using ProTasker.Application.Interfaces.Repositories;

namespace ProTasker.Infrastructure.Repositories
{
    public class ProjectTaskRepository : Repository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<ProjectTask>> GetAllAsync()
        {
            return await _dbSet
                .Include(t => t.AssignedUser)
                .Include(t => t.Team)
                .Include(t => t.Histories)
                .ThenInclude(h => h.PerformedByUser)
                .ToListAsync();
        }

        public override async Task<ProjectTask?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(t => t.AssignedUser)
                .Include(t => t.Team)
                .Include(t => t.Histories)
                .ThenInclude(h => h.PerformedByUser)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
