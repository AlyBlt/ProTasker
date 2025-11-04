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
    public class TaskHistoryConfiguration : IEntityTypeConfiguration<TaskHistory>
    {
        public void Configure(EntityTypeBuilder<TaskHistory> builder)
        {
            // TASK HISTORY → TASK (1:N)
            builder.HasOne(th => th.Task)
                   .WithMany(t => t.Histories)
                   .HasForeignKey(th => th.TaskId)
                   .OnDelete(DeleteBehavior.Restrict);

            // TASK HISTORY → PERFORMED BY USER (1:N)
            builder.Property(th => th.PerformedByUserId)
                   .IsRequired(false);

            // Optional: Tarih ve Action property’leri konfigürasyonu
            builder.Property(th => th.Action)
                   .IsRequired();


            builder.Property(th => th.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(th => th.OldValue)
                   .HasMaxLength(1000);

            builder.Property(th => th.NewValue)
                   .HasMaxLength(1000);
        }
    }
}
