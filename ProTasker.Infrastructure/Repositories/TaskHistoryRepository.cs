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
                .Select(h => new TaskHistory
                {
                    Id = h.Id,
                    TaskId = h.TaskId,
                    Task = h.Task, // navigation
                    PerformedByUserId = h.PerformedByUserId,
                    PerformedByUser = h.PerformedByUser, 
                    Action = h.Action,
                    OldValue = h.OldValue,
                    NewValue = h.NewValue,
                    CreatedAt = h.CreatedAt
                })
                .AsSplitQuery()
                .ToListAsync();
        }

        public override async Task<TaskHistory?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(h => h.Id == id)
                .Select(h => new TaskHistory
                {
                    Id = h.Id,
                    TaskId = h.TaskId,
                    Task = h.Task,
                    PerformedByUserId = h.PerformedByUserId,
                    PerformedByUser = h.PerformedByUser,
                    Action = h.Action,
                    OldValue = h.OldValue,
                    NewValue = h.NewValue,
                    CreatedAt = h.CreatedAt
                })
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }
    }
}
