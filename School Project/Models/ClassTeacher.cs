using System;
using System.Collections.Generic;

namespace School_Project.Models
{
    public partial class ClassTeacher
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = null!;
        public string? MathTeacher { get; set; }
        public string? PhysicsTeacher { get; set; }
        public string? ChemistryTeacher { get; set; }
    }
}
