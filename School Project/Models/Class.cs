using System;
using System.Collections.Generic;

namespace School_Project.Models
{
    public partial class Class
    {
        public int Id { get; set; }
        public string Class1 { get; set; } = null!;
        public int TimetableId { get; set; }
        public int MathTeacherId { get; set; }
        public int PhysicsTeacherId { get; set; }
        public int ChemistryTeacherId { get; set; }
    }
}
