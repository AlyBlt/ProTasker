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
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext context) : base(context) { }

        // Tüm takımları ID ve temel alanlar ile al
        public override async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _dbSet
                .Select(t => new Team
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    LeaderId = t.LeaderId
                })
                .ToListAsync();
        }

        // ID ile takım al
        public override async Task<Team?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Where(t => t.Id == id)
                .Select(t => new Team
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    LeaderId = t.LeaderId
                })
                .FirstOrDefaultAsync();
        }

        // Ek özellikler eklemek için-->> örn: takım üyelerinin ID’lerini çekmek
        public async Task<IEnumerable<Guid>> GetMemberIdsAsync(Guid teamId)
        {
            return await _context.Users
                .Where(u => u.TeamId == teamId)
                .Select(u => u.Id)
                .ToListAsync();
        }
    }
}