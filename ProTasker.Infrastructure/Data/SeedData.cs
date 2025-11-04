using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProTasker.Application.Models;
using ProTasker.Domain.Entities;
using ProTasker.Domain.Enums;
using System;

namespace ProTasker.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            // ---------------- USERS ----------------
            var adminUser = new ApplicationUser
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@protasker.com",
                NormalizedEmail = "ADMIN@PROTASKER.COM",
                Role = UserRole.Admin,
                EmailConfirmed = true
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123!");

            var teamLeader1 = new ApplicationUser
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                UserName = "TeamLeader1",
                NormalizedUserName = "TEAMLEADER1",
                Email = "teamleader1@protasker.com",
                NormalizedEmail = "TEAMLEADER1@PROTASKER.COM",
                Role = UserRole.TeamLeader,
                EmailConfirmed = true,
                TeamId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
            };
            teamLeader1.PasswordHash = hasher.HashPassword(teamLeader1, "Leader123!");

            var teamLeader2 = new ApplicationUser
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                UserName = "TeamLeader2",
                NormalizedUserName = "TEAMLEADER2",
                Email = "teamleader2@protasker.com",
                NormalizedEmail = "TEAMLEADER2@PROTASKER.COM",
                Role = UserRole.TeamLeader,
                EmailConfirmed = true,
                TeamId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")
            };
            teamLeader2.PasswordHash = hasher.HashPassword(teamLeader2, "Leader234!");

            var member1 = new ApplicationUser
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                UserName = "Member1",
                NormalizedUserName = "MEMBER1",
                Email = "member1@protasker.com",
                NormalizedEmail = "MEMBER1@PROTASKER.COM",
                Role = UserRole.Member,
                EmailConfirmed = true,
                TeamId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
            };
            member1.PasswordHash = hasher.HashPassword(member1, "Member123!");

            var member2 = new ApplicationUser
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                UserName = "Member2",
                NormalizedUserName = "MEMBER2",
                Email = "member2@protasker.com",
                NormalizedEmail = "MEMBER2@PROTASKER.COM",
                Role = UserRole.Member,
                EmailConfirmed = true,
                TeamId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
            };
            member2.PasswordHash = hasher.HashPassword(member2, "Member234!");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser, teamLeader1, teamLeader2, member1, member2);

            // ---------------- TEAMS ----------------
            var alphaTeamId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var betaTeamId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            var teams = new[]
            {
                new Team
                {
                    Id = alphaTeamId,
                    Name = "Alpha Team",
                    Description = "First Team",
                    LeaderId = teamLeader1.Id
                },
                new Team
                {
                    Id = betaTeamId,
                    Name = "Beta Team",
                    Description = "Second Team",
                    LeaderId = teamLeader2.Id
                }
            };
            modelBuilder.Entity<Team>().HasData(teams);

            // ---------------- TASKS ----------------
            var tasks = new[]
            {
                new ProjectTask
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Title = "Setup Project",
                    Description = "Initialize project repository and structure",
                    CreatedAt = new DateTime(2025, 10, 26, 12, 0, 0),
                    Status = ProjectTaskStatus.Todo,
                    TeamId = alphaTeamId,
                    AssignedUserId = member1.Id
                },
                new ProjectTask
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    Title = "Design Database",
                    Description = "Create database schema and tables",
                    CreatedAt = new DateTime(2025, 10, 26, 12, 30, 0),
                    Status = ProjectTaskStatus.InProgress,
                    TeamId = alphaTeamId,
                    AssignedUserId = member2.Id
                },
                new ProjectTask
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    Title = "API Implementation",
                    Description = "Develop REST API endpoints",
                    CreatedAt = new DateTime(2025, 10, 26, 13, 0, 0),
                    Status = ProjectTaskStatus.Todo,
                    TeamId = betaTeamId,
                    AssignedUserId = teamLeader2.Id
                }
            };
            modelBuilder.Entity<ProjectTask>().HasData(tasks);

            // ---------------- TASK HISTORIES ----------------
            var histories = new[]
            {
                new TaskHistory
                {
                    Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    TaskId = tasks[0].Id,
                    PerformedByUserId = member1.Id,
                    Action = TaskActionType.Created,
                    CreatedAt = new DateTime(2025, 10, 26, 12, 0, 0)
                },
                new TaskHistory
                {
                    Id = Guid.Parse("aaaaaaaa-ffff-ffff-ffff-ffffffffffff"),
                    TaskId = tasks[1].Id,
                    PerformedByUserId = member2.Id,
                    Action = TaskActionType.Updated,
                    CreatedAt = new DateTime(2025, 10, 26, 12, 45, 0)
                }
            };
            modelBuilder.Entity<TaskHistory>().HasData(histories);
        }
    }
}