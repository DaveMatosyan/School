using System;
using System.Collections.Generic;

namespace School_Project.Models
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public int DayId { get; set; }
        public int Hour { get; set; }
        public string Title { get; set; } = null!;

        public virtual Class Class { get; set; } = null!;
        public virtual User Teacher { get; set; } = null!;
    }
}
