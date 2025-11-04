using ProTasker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public ProjectTaskStatus Status { get; set; }=ProjectTaskStatus.Todo;

        // Foreign keys
        // N:1 — Görevin atandığı kullanıcı
        public Guid? AssignedUserId { get; set; }
      
        // N:1 — Görevin ait olduğu takım
        public Guid TeamId { get; set; } //can not be null
        public virtual Team Team { get; set; } = null!;

        // 1:N — Göreve ait geçmiş kayıtları
        public ICollection<TaskHistory> Histories { get; set; } = new List<TaskHistory>();
        
        
    }
}
