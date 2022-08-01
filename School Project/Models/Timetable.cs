using System;
using System.Collections.Generic;

namespace School_Project.Models
{
    public partial class Timetable
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = null!;
        public string Monday { get; set; } = null!;
        public string Tuesday { get; set; } = null!;
        public string Wednesday { get; set; } = null!;
        public string Thursday { get; set; } = null!;
        public string Friday { get; set; } = null!;
    }
}
