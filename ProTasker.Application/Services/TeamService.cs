using ProTasker.Application.Interfaces;
using ProTasker.Domain.Entities;
using ProTasker.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProTasker.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;

        public TeamService(ITeamRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Team>> GetAllTeamsAsync() => _repository.GetAllAsync();

        public Task<Team?> GetTeamByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task AddTeamAsync(Team team) => _repository.AddAsync(team);

        public Task UpdateTeamAsync(Team team) => _repository.UpdateAsync(team);

        public Task DeleteTeamAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
