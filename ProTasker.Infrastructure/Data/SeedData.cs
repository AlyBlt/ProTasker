using Microsoft.EntityFrameworkCore;
using ProTasker.Domain.Entities;
using ProTasker.Domain.Enums;
using System;

namespace ProTasker.Infrastructure.Data
{
    public static class SeedData
    {
        //Şimdilik Migration (HasData) ile yapıldı ama ilerde runtime (Program.cs + extension) denenebilir.
        public static void Seed(ModelBuilder modelBuilder)
        {
            // ---------------- USERS ----------------
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    UserName = "Admin",
                    Email = "admin@protasker.com",
                    PasswordHash = "hashedpassword",
                    Role = UserRole.Admin
                },
                new User
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    UserName = "TeamLeader",
                    Email = "teamleader@protasker.com",
                    PasswordHash = "hashedpassword",
                    Role = UserRole.TeamLeader
                },
                new User
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    UserName = "Member",
                    Email = "member@protasker.com",
                    PasswordHash = "hashedpassword",
                    Role = UserRole.Member
                },
                new User
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    UserName = "Alice",
                    Email = "alice@protasker.com",
                    PasswordHash = "hashedpassword",
                    Role = UserRole.Member
                },
                new User
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    UserName = "Bob",
                    Email = "bob@protasker.com",
                    PasswordHash = "hashedpassword",
                    Role = UserRole.TeamLeader
                }
            );

            // ---------------- TEAMS ----------------
            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Name = "Alpha Team",
                    Description = "First Team",
                    LeaderId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                },
                new Team
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Name = "Beta Team",
                    Description = "Second Team",
                    LeaderId = Guid.Parse("55555555-5555-5555-5555-555555555555")
                }
            );

            // ---------------- TASKS ----------------
            modelBuilder.Entity<ProjectTask>().HasData(
                new ProjectTask
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Title = "Setup Project",
                    Description = "Initialize project repository and structure",
                    CreatedAt = new DateTime(2025, 10, 26, 12, 0, 0),
                    Status = ProjectTaskStatus.Todo,
                    TeamId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    AssignedUserId = Guid.Parse("33333333-3333-3333-3333-333333333333")
                },
                new ProjectTask
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    Title = "Design Database",
                    Description = "Create database schema and tables",
                    CreatedAt = new DateTime(2025, 10, 26, 12, 30, 0),
                    Status = ProjectTaskStatus.InProgress,
                    TeamId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    AssignedUserId = Guid.Parse("44444444-4444-4444-4444-444444444444")
                },
                new ProjectTask
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    Title = "API Implementation",
                    Description = "Develop REST API endpoints",
                    CreatedAt = new DateTime(2025, 10, 26, 13, 0, 0),
                    Status = ProjectTaskStatus.Todo,
                    TeamId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    AssignedUserId = Guid.Parse("55555555-5555-5555-5555-555555555555")
                }
            );

            // ---------------- TASK HISTORIES ----------------
            modelBuilder.Entity<TaskHistory>().HasData(
                new TaskHistory
                {
                    Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    TaskId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    PerformedByUserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Action = TaskActionType.Created,
                    CreatedAt = new DateTime(2025, 10, 26, 12, 0, 0)
                },
                new TaskHistory
                {
                    Id = Guid.Parse("aaaaaaaa-ffff-ffff-ffff-ffffffffffff"),
                    TaskId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    PerformedByUserId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Action = TaskActionType.Updated,
                    CreatedAt = new DateTime(2025, 10, 26, 12, 45, 0)
                }
            );
        }
    }
}