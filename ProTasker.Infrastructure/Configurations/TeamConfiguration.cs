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
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            // TEAM NAME & DESCRIPTION
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Description)
                   .HasMaxLength(500);

            // TEAM → LEADER (1:1)
            builder.Property(t => t.LeaderId)
                  .IsRequired(false);

            
            // TEAM → PROJECTTASKS (1:N)
            builder.HasMany(t => t.Tasks)
                   .WithOne(pt => pt.Team)
                   .HasForeignKey(pt => pt.TeamId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
