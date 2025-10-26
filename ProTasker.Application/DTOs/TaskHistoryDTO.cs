using ProTasker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.DTOs
{
    public class TaskHistoryDTO
    {
        public Guid Id { get; set; }
        public Guid? TaskId { get; set; }
        public TaskActionType Action { get; set; } = TaskActionType.Created;
        public string? PerformedByUserName { get; set; }
    }
}
