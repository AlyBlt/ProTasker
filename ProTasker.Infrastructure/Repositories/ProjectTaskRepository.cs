using Microsoft.EntityFrameworkCore;
using ProTasker.Application.DTOs;
using ProTasker.Application.Interfaces.Repositories;
using ProTasker.Application.Models;
using ProTasker.Domain.Entities;
using ProTasker.Domain.Enums;
using ProTasker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Infrastructure.Repositories
{
    public class ProjectTaskRepository : Repository<ProjectTask>, IProjectTaskRepository
    {
        private readonly AppDbContext _context;
        public ProjectTaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<ProjectTask>> GetAllAsync()
        {
            return await _context.Tasks
              .Include(t => t.Team)
              .Include(t => t.Histories)
              .ToListAsync();

        }

        public override async Task<ProjectTask?> GetByIdAsync(Guid id)
        {

            return await _context.Tasks
                .Include(t => t.Team)
                .Include(t => t.Histories)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        
    }
}
