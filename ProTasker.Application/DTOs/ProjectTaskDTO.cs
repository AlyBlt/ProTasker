using ProTasker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProTasker.Application.DTOs
{
    public class ProjectTaskDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Todo;

        public string? AssignedUserName { get; set; }
        public string? TeamName { get; set; }
    }
}
