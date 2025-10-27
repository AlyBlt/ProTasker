using ProTasker.Application.Interfaces.Repositories;
using ProTasker.Application.Interfaces.Services;
using ProTasker.Domain.Entities;
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

        public Task<IEnumerable<Team>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Team?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task AddAsync(Team team) => _repository.AddAsync(team);

        public Task UpdateAsync(Team team) => _repository.UpdateAsync(team);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
