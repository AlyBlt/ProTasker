using ProTasker.Application.Interfaces;
using ProTasker.Domain.Entities;
using ProTasker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace ProTasker.Infrastructure.Repositories
{
    public class TaskHistoryRepository : ITaskHistoryRepository
    {
        private readonly AppDbContext _context;

        public TaskHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskHistory>> GetAllAsync()
        {
            return await _context.TaskHistories
                .Include(h => h.Task)
                .Include(h => h.PerformedByUser)
                .ToListAsync();
        }

        public async Task<TaskHistory?> GetByIdAsync(Guid id)
        {
            return await _context.TaskHistories
                .Include(h => h.Task)
                .Include(h => h.PerformedByUser)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task AddAsync(TaskHistory history)
        {
            _context.TaskHistories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskHistory history)
        {
            _context.TaskHistories.Update(history);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var history = await _context.TaskHistories.FindAsync(id);
            if (history != null)
            {
                _context.TaskHistories.Remove(history);
                await _context.SaveChangesAsync();
            }
        }
    }
}
