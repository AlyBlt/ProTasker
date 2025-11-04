using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProTasker.Application.Models;
using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Infrastructure.Configurations
{
    public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            // TEAM
            builder.HasOne(t => t.Team)
                   .WithMany(team => team.Tasks)
                   .HasForeignKey(t => t.TeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ASSIGNED USER (ApplicationUser üzerinden)
            builder.Property(t => t.AssignedUserId)
                   .IsRequired(false);

            // TASK HISTORIES
            builder.HasMany(t => t.Histories)
                   .WithOne(th => th.Task)
                   .HasForeignKey(th => th.TaskId)
                   .OnDelete(DeleteBehavior.Restrict);

            // ---------------- PROPERTIES ----------------
            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            builder.Property(t => t.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(t => t.Status)
                   .IsRequired();
        }
    }
}
