using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProTasker.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // ---------------- TEAM ----------------
            // User hangi takıma ait
            builder.Property(u => u.TeamId)
                   .IsRequired(false); // Null olabilir, team optional

            // ---------------- PROJECT TASKS ----------------
            // User’a atanmış görevler
            builder.HasMany(u => u.Tasks)
                   .WithOne() // Navigation property artık yok
                   .HasForeignKey(t => t.AssignedUserId)
                   .OnDelete(DeleteBehavior.SetNull); // User silinse AssignedUserId null olur

            // ---------------- TASK HISTORIES ----------------
            // User’ın yaptığı aksiyonlar
            builder.HasMany(u => u.TaskHistories)
                   .WithOne() // Navigation property artık yok
                   .HasForeignKey(th => th.PerformedByUserId)
                   .OnDelete(DeleteBehavior.SetNull); // User silinse PerformById null olur
        }
    }
}

