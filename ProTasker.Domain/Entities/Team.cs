using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Domain.Entities
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;

        // Team Leader (A user)
        public Guid LeaderId { get; set; }
        public User? Leader { get; set; }

        // Navigational properties
        public ICollection<User> Members { get; set; } = new List<User>(); //A team may include more members
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>(); //Tasks for team
    }
}
