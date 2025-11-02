using Microsoft.EntityFrameworkCore;
using ProTasker.Application.Interfaces.Repositories;
using ProTasker.Application.Models;  // ApplicationUser'ı buradan alıyoruz
using ProTasker.Domain.Entities;
using ProTasker.Infrastructure.Data;  // DbContext
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProTasker.Infrastructure.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository  // ApplicationUser'ı kullanıyoruz
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _dbSet
                .Select(u => new ApplicationUser
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Team = u.Team,
                    Tasks = u.Tasks
                        .Select(t => new ProjectTask
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Status = t.Status,
                            AssignedUserId = t.AssignedUserId
                        }).ToList(),
                    TaskHistories = u.TaskHistories
                        .Select(h => new TaskHistory
                        {
                            Id = h.Id,
                            TaskId = h.TaskId,
                            PerformedByUserId = h.PerformedByUserId,
                            Action = h.Action,
                            OldValue = h.OldValue,
                            NewValue = h.NewValue,
                            CreatedAt = h.CreatedAt
                        }).ToList()
                })
                .AsSplitQuery()
                .ToListAsync();
        }

        public override async Task<ApplicationUser?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(u => u.Id == id)
                .Select(u => new ApplicationUser
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Team = u.Team,
                    Tasks = u.Tasks
                        .Select(t => new ProjectTask
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Status = t.Status,
                            AssignedUserId = t.AssignedUserId
                        }).ToList(),
                    TaskHistories = u.TaskHistories
                        .Select(h => new TaskHistory
                        {
                            Id = h.Id,
                            TaskId = h.TaskId,
                            PerformedByUserId = h.PerformedByUserId,
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