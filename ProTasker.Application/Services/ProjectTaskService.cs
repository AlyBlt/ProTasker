using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProTasker.Application.DTOs;
using ProTasker.Application.Exceptions;
using ProTasker.Application.Interfaces.Repositories;
using ProTasker.Application.Interfaces.Services;
using ProTasker.Application.Models;
using ProTasker.Domain.Entities;
using ProTasker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _taskRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectTaskService(IProjectTaskRepository taskRepository, ITeamRepository teamRepository,
            UserManager<ApplicationUser> userManager)
        {
            _taskRepository = taskRepository;
            _teamRepository = teamRepository;
            _userManager = userManager;
        }

        public Task<IEnumerable<ProjectTask>> GetAllAsync() => _taskRepository.GetAllAsync();

        public Task<ProjectTask?> GetByIdAsync(Guid id) => _taskRepository.GetByIdAsync(id);

        public async Task AddAsync(ProjectTask task)
        {
            // Takım kontrolü
            var team = await _teamRepository.GetByIdAsync(task.TeamId)
                ?? throw new ArgumentException($"Team with Id {task.TeamId} does not exist.");

            // AssignedUser kontrolü
            if (task.AssignedUserId.HasValue)
            {
                var appUser = await _userManager.FindByIdAsync(task.AssignedUserId.Value.ToString())
                    ?? throw new ArgumentException($"User with Id {task.AssignedUserId.Value} does not exist.");

                if (appUser.TeamId != task.TeamId)
                    throw new InvalidOperationException(
                        $"User '{appUser.UserName}' does not belong to Team '{team.Name}'.");
            }

            // Varsayılan değerler
            task.Status = ProjectTaskStatus.Todo;
            task.CreatedAt = DateTime.UtcNow;
            task.Team = null;

            await _taskRepository.AddAsync(task);
        }

        public async Task UpdateAsync(ProjectTask task)
        {
            var existingTask = await _taskRepository.GetByIdAsync(task.Id)
                ?? throw new NotFoundException($"Task with Id {task.Id} does not exist.");

            // Team kontrolü
            if (task.TeamId != existingTask.TeamId)
            {
                var team = await _teamRepository.GetByIdAsync(task.TeamId)
                    ?? throw new ValidationException($"Team with Id {task.TeamId} does not exist.");
            }

            // AssignedUser kontrolü
            if (task.AssignedUserId.HasValue)
            {
                var appUser = await _userManager.FindByIdAsync(task.AssignedUserId.Value.ToString())
                    ?? throw new ValidationException($"User with Id {task.AssignedUserId.Value} does not exist.");

                if (appUser.TeamId != task.TeamId)
                    throw new ValidationException(
                        $"User '{appUser.UserName}' does not belong to Team with Id {task.TeamId}.");
            }

            // Güncelleme
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.DueDate = task.DueDate;
            existingTask.AssignedUserId = task.AssignedUserId;
            existingTask.TeamId = task.TeamId;

            await _taskRepository.UpdateAsync(existingTask);
        }

        public Task<bool> DeleteAsync(Guid id) => _taskRepository.DeleteAsync(id);

        public async Task<ApplicationUser?> GetUserByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }


    }
}
