using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Enums;



namespace ProTasker.Domain.Entities
{
    public class ProjectTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public ProjectTaskStatus Status { get; set; }=ProjectTaskStatus.Todo;

        // Foreign keys
        public Guid? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }  //who is resposible for the job

        public Guid TeamId { get; set; } //can not be null
        public Team Team { get; set; } = null!;

        public ICollection<TaskHistory> Histories { get; set; } = new List<TaskHistory>();
        
        
    }
}
