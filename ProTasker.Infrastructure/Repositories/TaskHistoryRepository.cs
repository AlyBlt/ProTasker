using ProTasker.Domain.Entities;
using ProTasker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProTasker.Domain.Enums;
using ProTasker.Application.Interfaces.Repositories;



namespace ProTasker.Infrastructure.Repositories
{
    public class TaskHistoryRepository : Repository<TaskHistory>, ITaskHistoryRepository
    {
        public TaskHistoryRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<TaskHistory>> GetAllAsync()
        {
            return await _dbSet
                .Include(h => h.Task)
                .Include(h => h.PerformedByUser)
                .ToListAsync();
        }

        public override async Task<TaskHistory?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(h => h.Task)
                .Include(h => h.PerformedByUser)
                .FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
