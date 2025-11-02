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
                .Select(t => new ProjectTask
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    AssignedUserId = t.AssignedUserId,
                    AssignedUser = t.AssignedUser,   // navigation
                    TeamId = t.TeamId,
                    Team = t.Team,                   // navigation
                    Histories = t.Histories
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
                        }).ToList()
                })
                .AsSplitQuery()
                .ToListAsync();
        }

        public override async Task<ProjectTask?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(t => t.Id == id)
                .Select(t => new ProjectTask
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    AssignedUserId = t.AssignedUserId,
                    AssignedUser = t.AssignedUser,   // navigation
                    TeamId = t.TeamId,
                    Team = t.Team,                   // navigation
                    Histories = t.Histories
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
                        }).ToList()
                })
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }
    }
}
