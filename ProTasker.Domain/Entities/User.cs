using ProTasker.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ProTasker.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }  // Unique identifier
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Roles: Admin, TeamLeader, Member
        public UserRole Role { get; set; } = UserRole.Member;

        // N:1 — Kullanıcı bir takıma aittir
        // FK //Team that user belongs to
        public Guid? TeamId { get; set; }
        public virtual Team? Team { get; set; }

     
        // Navigational properties
        // 1:N — Kullanıcıya atanmış görevler
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>(); //AssignedTasks
        
        // 1:N — Kullanıcının gerçekleştirdiği task geçmişleri
        public ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();
        
    }
}
