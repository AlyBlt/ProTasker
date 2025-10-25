using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Domain.Entities
{
    public class TaskHistory
    {
        public Guid Id { get; set; }
        public Guid? TaskId { get; set; }
        public ProjectTask? Task { get; set; } = null!;
        public string Action { get; set; } = string.Empty;  // örn: "Created", "Completed"
        public DateTime ActionDate { get; set; } = DateTime.UtcNow;
        public Guid? PerformedByUserId { get; set; }
        public User? PerformedByUser { get; set; } = null!;
    }
}
