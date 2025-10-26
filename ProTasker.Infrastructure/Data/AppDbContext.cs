using Microsoft.EntityFrameworkCore;
using ProTasker.Domain.Entities;
using ProTasker.Domain.Enums;

namespace ProTasker.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<ProjectTask> Tasks { get; set; } = null!;
        public DbSet<TaskHistory> TaskHistories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------------- ENUM CONVERSIONS ----------------
            // Keep the enums as string
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<ProjectTask>()
                .Property(t => t.Status) 
                .HasConversion<string>();

            modelBuilder.Entity<TaskHistory>()
                .Property(th => th.Action)
                .HasConversion<string>();

            //--------------------------------------------------------
            //--------------RELATIONS---------------------------------


            //Other Fluent API Relations

            //TEAM — MEMBERS (1:N)
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithOne(u => u.Team)
                .HasForeignKey(u => u.TeamId)
                .OnDelete(DeleteBehavior.SetNull);

            //TEAM — LEADER (1:1)
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Leader)
                .WithMany()
                .HasForeignKey(t => t.LeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            //TASK — TEAM (1:N)
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.Team)
                .WithMany(team => team.Tasks)
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            //TASK — ASSIGNED USER (N:1)
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.AssignedUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            //TASKHISTORY — PROJECTTASK (1:N)
            modelBuilder.Entity<TaskHistory>()
                .HasOne(th => th.Task)
                .WithMany(t => t.Histories)
                .HasForeignKey(th => th.TaskId)
                .OnDelete(DeleteBehavior.SetNull);

            //TASKHISTORY — PERFORMEDBYUSER (1:N)
            modelBuilder.Entity<TaskHistory>()
                .HasOne(th => th.PerformedByUser)
                .WithMany(u => u.TaskHistories)
                .HasForeignKey(th => th.PerformedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // ---------------- SEED DATA ----------------
            SeedData.Seed(modelBuilder); 

        }

    }
}
