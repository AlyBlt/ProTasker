using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Entities;
using AutoMapper;
using ProTasker.Application.DTOs;
using ProTasker.Application.Interfaces.Repositories;
using ProTasker.Application.Interfaces.Services;

namespace ProTasker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<User>> GetAllAsync() => _repository.GetAllAsync();

        public Task<User?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task AddAsync(User user) => _repository.AddAsync(user);

        public Task UpdateAsync(User user) => _repository.UpdateAsync(user);

        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
