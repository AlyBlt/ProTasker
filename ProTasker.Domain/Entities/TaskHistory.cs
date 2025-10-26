using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProTasker.Domain.Enums;

namespace ProTasker.Domain.Entities
{
    public class TaskHistory
    {
        public Guid Id { get; set; }
        public Guid? TaskId { get; set; }
        public ProjectTask? Task { get; set; }
        public Guid? PerformedByUserId { get; set; }
        public User? PerformedByUser { get; set; }

        public TaskActionType Action { get; set; } = TaskActionType.Created;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
