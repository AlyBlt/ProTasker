using Microsoft.EntityFrameworkCore;
using ProTasker.Application.Interfaces;
using ProTasker.Domain.Entities;
using ProTasker.Infrastructure.Data;
using ProTasker.Infrastructure.Interfaces;
using ProTasker.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProTasker.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await _context.Teams
                .Include(t => t.Members)
                .Include(t => t.Tasks)
                .ToListAsync();
        }

        public async Task<Team?> GetByIdAsync(Guid id)=>await _context.Teams
            .Include(t => t.Members)
            .Include(t => t.Tasks)
            .FirstOrDefaultAsync(t => t.Id == id);




        public async Task AddAsync(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }
    }
}
