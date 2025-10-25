using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProTasker.Domain.Entities
{
    public class ProjectTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;

        // Foreign keys
        public Guid? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }

        public Guid? TeamId { get; set; }
        public Team? Team { get; set; } = null!;

        public ICollection<TaskHistory> Histories { get; set; } = new List<TaskHistory>();
        
        
        //public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
