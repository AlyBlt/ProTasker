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
                .Include(t => t.Members)
                .Include(t => t.Tasks)
                .ToListAsync();
        }

        public override async Task<Team?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(t => t.Members)
                .Include(t => t.Tasks)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

    }
}
