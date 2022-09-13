using Microsoft.EntityFrameworkCore;
using School_Project.Models;

namespace School_Project.Services
{
    public class StudentServices
    {

        public static User [] GetStudentsByTeacher(int TeacherId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var list = TeacherServices.TeachersClassesIds(TeacherId);
                var items = db.Users.Where(b => list.Contains(b.ClassId)).ToArray();
                return items;
            }
        }

    }
}
