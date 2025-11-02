using ProTasker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.DTOs
{
    public class TeamDTO
    {
        public Guid Id { get; set; }
       
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string LeaderName { get; set; }= string.Empty;
    }
}
