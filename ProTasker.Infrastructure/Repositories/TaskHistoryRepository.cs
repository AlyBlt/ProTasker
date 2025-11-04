using Microsoft.EntityFrameworkCore;
using ProTasker.Application.Interfaces.Repositories;
using ProTasker.Domain.Entities;
using ProTasker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProTasker.Infrastructure.Repositories
{
    public class TaskHistoryRepository : Repository<TaskHistory>, ITaskHistoryRepository
    {
        public TaskHistoryRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<TaskHistory>> GetAllAsync()
        {
            return await _dbSet
                .AsSplitQuery()
                .ToListAsync();
        }

        public override async Task<TaskHistory?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}