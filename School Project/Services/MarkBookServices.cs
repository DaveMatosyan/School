using School_Project.Models;

namespace School_Project.Services
{
    public class MarkBookServices
    {
        public static Grade[] GetGrades(string Title, int ClassId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var grades = db.Grades.Where(b => b.Student.ClassId == ClassId && b.Title == Title).ToArray();
                return grades;
            }
        }
    }
}
