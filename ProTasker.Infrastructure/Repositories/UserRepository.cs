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
            .Include(u => u.Tasks)
            .Include(u => u.TaskHistories)
            .Include(u => u.Team)
            .ToListAsync();
        }

        public override async Task<ApplicationUser?> GetByIdAsync(Guid id)
        {
            return await _dbSet
            .Include(u => u.Tasks)
            .Include(u => u.TaskHistories)
            .Include(u => u.Team)
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ApplicationUser?> GetByUserNameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }
}