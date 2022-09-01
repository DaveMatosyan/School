using Microsoft.EntityFrameworkCore;
using School_Project.Models;
using School_Project.Services;
namespace School_Project.Services
{
    public class TeacherServices
    {
        public static dynamic GetTeachersTodaysLessons(int TeacherId)
        {
            DateTime ClockInfoFromSystem = DateTime.Now;
            int WeekDayIndex = (int)ClockInfoFromSystem.DayOfWeek;
            var teacher = UserServices.GetUserById(TeacherId);
            using (SchoolContext db = new SchoolContext())
            {

                var list = db.Schedules.Where(b => b.TeacherId == TeacherId && b.DayId == WeekDayIndex).Include("Class").OrderBy(o => o.Hour).ToList();
                return list;
            }
        }
        public static dynamic GetTeachers()
        {
            using (SchoolContext db = new SchoolContext())
            {

                var list = db.Users.Where(b => b.Profession != null && b.Profession != "Principal").ToList();
                return list;
            }
        }
        public static void Edit(User Teacher)
        {
            if(Teacher.FirstName==null||Teacher.LastName==null||Teacher.Username==null)
            {
                return;
            }
            var OldTeacher = UserServices.GetUserById(Teacher.Id);
            if (Teacher.Password == null)
            {
                Teacher.Password = OldTeacher.Password;
            } else
            {
                Teacher.Password = HashServices.HashPassword(Teacher.Password);

            }
            using (SchoolContext db = new SchoolContext())
            {
                db.Update(Teacher);
                db.SaveChanges();
            }
        }
        public static void DeleteTeacher(int TeacherId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var itemsToRemove = db.Schedules.Where(x => x.TeacherId == TeacherId); //returns a single item.
                var Teacher = db.Users.Find(TeacherId);
                db.Schedules.RemoveRange(itemsToRemove);
                db.Users.Remove(Teacher);
                db.SaveChanges();
            }
        }
    }
}
