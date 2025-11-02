using ProTasker.Application.Interfaces.Repositories;
using ProTasker.Application.Interfaces.Services;
using ProTasker.Application.Models;  // ApplicationUser'ı burada alıyoruz
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProTasker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ApplicationUser>> GetAllAsync() => _repository.GetAllAsync();

        public Task<ApplicationUser?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task AddAsync(ApplicationUser user) => _repository.AddAsync(user);

        public Task UpdateAsync(ApplicationUser user) => _repository.UpdateAsync(user);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}