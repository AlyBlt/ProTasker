using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProTasker.Infrastructure.Data; // DbContext 
using ProTasker.Domain.Enums;
using ProTasker.Application.Interfaces.Repositories;


namespace ProTasker.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        
        public UserRepository(AppDbContext context) : base(context) { }


        //only for user special requests--for the crud actions-it uses base repository
        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbSet
                .Include(u => u.Tasks)
                .Include(u => u.TaskHistories)
                .Include(u => u.Team)
                .ToListAsync();
        }

        public override async Task<User?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(u => u.Tasks)
                .Include(u => u.TaskHistories)
                .Include(u => u.Team)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}