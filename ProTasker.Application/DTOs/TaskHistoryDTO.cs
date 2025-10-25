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
        public string Action { get; set; } = string.Empty;
        public DateTime ActionDate { get; set; }

        public Guid? PerformedByUserId { get; set; }
        public string? PerformedByUserName { get; set; }
    }
}
