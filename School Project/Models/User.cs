using System;
using System.Collections.Generic;

namespace School_Project.Models
{
    public partial class User
    {
        public User()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int? ClassId { get; set; }
        public string? Profession { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
