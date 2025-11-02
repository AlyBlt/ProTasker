using Microsoft.AspNetCore.Identity;
using ProTasker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        //public Guid Id { get; set; }  // Unique identifier
        //public string Name { get; set; } = string.Empty;

        // Enum Role
        [NotMapped]
        public UserRole UserRole { get; set; } // UserRole enum'ını burada ilişkilendiriyoruz
    }
}