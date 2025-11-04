using ProTasker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProTasker.Domain.Entities
{
    public class TaskHistory
    {
        public Guid Id { get; set; }

        // N:1 — Hangi görevle ilişkili
        public Guid TaskId { get; set; }
        public virtual ProjectTask? Task { get; set; }

        // N:1 — Hangi kullanıcı işlemi yaptı
        public Guid? PerformedByUserId { get; set; }
        public TaskActionType Action { get; set; } = TaskActionType.Created;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
