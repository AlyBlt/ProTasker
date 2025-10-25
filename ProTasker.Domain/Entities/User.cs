using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }  // Unique identifier
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Navigational properties
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

        public ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();
        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
