using Microsoft.EntityFrameworkCore;
using ProTasker.Domain.Entities;
using ProTasker.Application.Models;
using ProTasker.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProTasker.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        // DbSet tanımlamaları
        //public DbSet<ApplicationUser> Users { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<ProjectTask> Tasks { get; set; } = null!;
        public DbSet<TaskHistory> TaskHistories { get; set; } = null!;
        
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskHistory>()
                .Ignore(th => th.PerformedByUser); // Domain User’ı ignore et

            modelBuilder.Entity<TaskHistory>()
                .Property<Guid?>("PerformedByUserId"); // Shadow property olarak GUID oluştur

            modelBuilder.Entity<TaskHistory>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.TaskHistories)
                .HasForeignKey("PerformedByUserId") // string ile shadow property
                .OnDelete(DeleteBehavior.SetNull);

            // ---------------- RELATIONS ----------------

            // Team - Leader (ApplicationUser ile)
            modelBuilder.Entity<Team>()
                .HasOne<ApplicationUser>()  // sadece FK, navigation yok
                .WithMany()
                .HasForeignKey(t => t.LeaderId)
                .OnDelete(DeleteBehavior.SetNull);

            // Team → Members (ApplicationUser ile)
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithOne(u => u.Team)
                .HasForeignKey(u => u.TeamId)
                .OnDelete(DeleteBehavior.SetNull);

            // Team → ProjectTasks
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Tasks)
                .WithOne(pt => pt.Team)
                .HasForeignKey(pt => pt.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProjectTask → AssignedUser (ApplicationUser ile)
            modelBuilder.Entity<ProjectTask>()
                .HasOne<ApplicationUser>()  // navigation yok
                .WithMany(u => u.Tasks)
                .HasForeignKey(pt => pt.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // ProjectTask → TaskHistories
            modelBuilder.Entity<ProjectTask>()
                .HasMany(pt => pt.Histories)
                .WithOne(th => th.Task)
                .HasForeignKey(th => th.TaskId)
                .OnDelete(DeleteBehavior.SetNull);

            // TaskHistory → PerformedByUser (ApplicationUser ile)
            modelBuilder.Entity<TaskHistory>()
                .HasOne<ApplicationUser>()  // navigation yok
                .WithMany(u => u.TaskHistories)
                .HasForeignKey(th => th.PerformedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // ---------------- ENUM CONVERSIONS ----------------
            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<ProjectTask>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<TaskHistory>()
                .Property(th => th.Action)
                .HasConversion<string>();

            // ---------------- SEED DATA ----------------
            SeedData.Seed(modelBuilder);
        }
    }
}