using System;
using System.Collections.Generic;

namespace School_Project.Models
{
    public partial class Class
    {
        public Class()
        {
            Schedules = new HashSet<Schedule>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Class1 { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
