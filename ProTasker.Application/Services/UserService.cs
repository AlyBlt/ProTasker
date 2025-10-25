using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Application.Interfaces;
using ProTasker.Domain.Entities;
using AutoMapper;
using ProTasker.Application.DTOs;

namespace ProTasker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<List<User>> GetAllUsersAsync() => _repository.GetAllAsync();
        public Task<User?> GetUserByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public Task AddUserAsync(User user) => _repository.AddAsync(user);
        public Task UpdateUserAsync(User user) => _repository.UpdateAsync(user);
        public Task DeleteUserAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
