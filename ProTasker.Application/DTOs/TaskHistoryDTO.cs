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
    public class TaskHistoryDTO
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskActionType Action { get; set; } = TaskActionType.Created;
        public Guid? PerformedByUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
