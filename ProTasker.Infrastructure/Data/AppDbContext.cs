using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProTasker.Application.Models;
using ProTasker.Domain.Entities;
using ProTasker.Domain.Enums;
using ProTasker.Infrastructure.Configurations;

namespace ProTasker.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        // DbSet tanımlamaları
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<ProjectTask> Tasks { get; set; } = null!;
        public DbSet<TaskHistory> TaskHistories { get; set; } = null!;
        
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Domain.User'ı EF tabloya çevirmesin — çünkü ApplicationUser ile çalışıyoruz.
            modelBuilder.Ignore<User>();
            modelBuilder.ApplyConfiguration(new ProjectTaskConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new TaskHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());


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