using System;
using System.Collections.Generic;

namespace School_Project.Models
{
    public partial class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Day { get; set; }
        public int Hour { get; set; }
        public string Title { get; set; } = null!;
        public int Grade1 { get; set; }

        public virtual User Student { get; set; } = null!;
    }
}
