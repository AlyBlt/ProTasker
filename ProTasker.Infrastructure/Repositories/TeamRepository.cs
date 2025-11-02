using Microsoft.EntityFrameworkCore;
using ProTasker.Domain.Entities;
using ProTasker.Infrastructure.Data;
using ProTasker.Application.Interfaces.Repositories;
using ProTasker.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Enums;



namespace ProTasker.Infrastructure.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _dbSet
                .Select(t => new Team
                {
                    Id = t.Id,
                    Name = t.Name,
                    Members = t.Members.ToList(),  // eager load members
                    Tasks = t.Tasks
                        .Select(task => new ProjectTask
                        {
                            Id = task.Id,
                            Title = task.Title,
                            Description = task.Description,
                            Status = task.Status,
                            AssignedUserId = task.AssignedUserId,
                            AssignedUser = task.AssignedUser,
                            Histories = task.Histories
                                .Select(h => new TaskHistory
                                {
                                    Id = h.Id,
                                    TaskId = h.TaskId,
                                    PerformedByUserId = h.PerformedByUserId,
                                    PerformedByUser = h.PerformedByUser,
                                    Action = h.Action,
                                    OldValue = h.OldValue,
                                    NewValue = h.NewValue,
                                    CreatedAt = h.CreatedAt
                                }).ToList()
                        }).ToList()
                })
                .AsSplitQuery()
                .ToListAsync();
        }

        public override async Task<Team?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(t => t.Id == id)
                .Select(t => new Team
                {
                    Id = t.Id,
                    Name = t.Name,
                    Members = t.Members.ToList(),
                    Tasks = t.Tasks
                        .Select(task => new ProjectTask
                        {
                            Id = task.Id,
                            Title = task.Title,
                            Description = task.Description,
                            Status = task.Status,
                            AssignedUserId = task.AssignedUserId,
                            AssignedUser = task.AssignedUser,
                            Histories = task.Histories
                                .Select(h => new TaskHistory
                                {
                                    Id = h.Id,
                                    TaskId = h.TaskId,
                                    PerformedByUserId = h.PerformedByUserId,
                                    PerformedByUser = h.PerformedByUser,
                                    Action = h.Action,
                                    OldValue = h.OldValue,
                                    NewValue = h.NewValue,
                                    CreatedAt = h.CreatedAt
                                }).ToList()
                        }).ToList()
                })
                .AsSplitQuery()
                .FirstOrDefaultAsync();
        }

    }
}
