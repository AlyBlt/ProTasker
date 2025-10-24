using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Domain.Entities
{
    internal class User
    {
        public Guid Id { get; set; }  // Unique identifier
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Navigational properties
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
