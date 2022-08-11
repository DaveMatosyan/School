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
        public static dynamic GetTeachersLessonsByWeekday(int TeacherId, int WeekdayId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var list = db.Schedules.Where(b => b.TeacherId == TeacherId && b.DayId == WeekdayId).Include("Class").OrderBy(o => o.Hour).ToList();
                return list;
            }
        }
    }
}
