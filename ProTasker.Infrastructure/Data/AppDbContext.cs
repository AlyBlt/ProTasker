using Microsoft.EntityFrameworkCore;
using ProTasker.Domain.Entities;

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

            modelBuilder.Entity<ProjectTask>()
            .HasOne(t => t.AssignedUser)
            .WithMany()
            .HasForeignKey(t => t.AssignedUserId)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskHistory>()
                .HasOne(th => th.PerformedByUser)
                .WithMany()
                .HasForeignKey(th => th.PerformedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskHistory>()
                .HasOne(th => th.Task)
                .WithMany(t => t.Histories)
                .HasForeignKey(th => th.TaskId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
            .HasMany(u => u.Teams)
            .WithMany(t => t.Members)
            .UsingEntity(j => j.ToTable("UserTeams"));
        }

        

    }
}
