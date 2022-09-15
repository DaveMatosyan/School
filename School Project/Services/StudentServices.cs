using Microsoft.EntityFrameworkCore;
using School_Project.Models;

namespace School_Project.Services
{
    public class StudentServices
    {

        public static User [] GetStudentsByTeacherAndClass(int TeacherId, int ClassId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var list = TeacherServices.TeachersClassesIds(TeacherId);
                var items = db.Users.Where(b => list.Contains(b.ClassId) && b.ClassId == ClassId).ToArray();
                return items;
            }
        }
        public static User[] GetStudentsByClass(int ClassId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var items = db.Users.Where(b => b.ClassId == ClassId).ToArray();
                return items;
            }
        }
        static public void PostStudent(User user)
        {
            using (var context = new SchoolContext())
            {
                user.Password = HashServices.HashPassword(user.Password);
                user.Role = "Student";
                context.Add(user);
                context.SaveChanges();
            }
        }
    }
}
