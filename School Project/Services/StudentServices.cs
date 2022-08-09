using School_Project.Models;

namespace School_Project.Services
{
    public class StudentServices
    {
        public static dynamic GetTeachersLessonsByWeekday(int StudentId, int WeekdayId)
        {
            int? classId = UserServices.GetUserById(StudentId).ClassId;
            using (SchoolContext db = new SchoolContext())
            {
                var list = db.Schedules.Where(b => b.Class.Id == classId && b.DayId == WeekdayId).OrderBy(o => o.Hour).ToList();
                return list;
            }
        }
    }
}
