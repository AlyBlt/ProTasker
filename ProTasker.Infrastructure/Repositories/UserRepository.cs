using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Entities;
using ProTasker.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProTasker.Infrastructure.Data; // DbContext için

namespace ProTasker.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync() => await _context.Users
            .Include(u => u.Tasks)
            .Include(u => u.TaskHistories)
            .Include(u => u.Teams)
            .ToListAsync();
        public async Task<User?> GetByIdAsync(Guid id) => await _context.Users
            .Include(u => u.Tasks)
            .Include(u => u.TaskHistories)
            .Include(u => u.Teams)
            .FirstOrDefaultAsync(u => u.Id == id);
        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}