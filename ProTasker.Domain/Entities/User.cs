using ProTasker.Domain.Enums;

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

        // FK //Team that user belongs to
        public Guid? TeamId { get; set; }
        public Team? Team { get; set; }

        // Navigational properties
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>(); //AssignedTasks

        public ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();
        
    }
}
