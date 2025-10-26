using ProTasker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.DTOs
{
    public class ProjectTaskDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Todo;

        public string? AssignedUserName { get; set; }
        public string? TeamName { get; set; }
    }
}
