using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Guid? LeaderId { get; set; }
               
        // Navigational properties
        
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>(); //Tasks for team
    }
}
