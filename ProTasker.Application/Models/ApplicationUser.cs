using Microsoft.AspNetCore.Identity;
using ProTasker.Domain.Entities;
using ProTasker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProTasker.Application.Models
{
    public class ApplicationUser : IdentityUser<Guid>  
    {
       
        public UserRole Role { get; set; } = UserRole.Member;

        // FK //Team that user belongs to
        public Guid? TeamId { get; set; }
        public Team? Team { get; set; }

        // Navigational properties
        public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>(); //AssignedTasks

        public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();
    }
}
