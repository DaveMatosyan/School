using School_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace School_Project.Services
{
    public class ScheduleServices
    {
        public static List<Schedule> GetScedulesByClassId(int ClassId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var list = db.Schedules.Where(b => b.ClassId == ClassId).OrderBy(o => o.Hour).OrderBy(o => o.DayId).ToList();
                return list;
            }
        }
        public static void DeleteSchedule(int SchdeuleId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var itemToRemove = db.Schedules.SingleOrDefault(x => x.Id == SchdeuleId); //returns a single item.

                if (itemToRemove != null)
                {
                    db.Schedules.Remove(itemToRemove);
                    db.SaveChanges();
                }
            }
        }
        public static void PostSchedule(AddSchedule addSchedule,int ClassId, int UserId, string Title)
        {
            Schedule schedule = new();
            schedule.ClassId = ClassId;
            schedule.TeacherId = UserId;
            schedule.Hour = addSchedule.Hour;
            schedule.DayId = addSchedule.DayId;
            schedule.Title = Title+"Hour";

            using (SchoolContext db = new SchoolContext())
            {
                db.Add(schedule);
                db.SaveChanges();
            }
        }
        public static dynamic GetschedulesByClassId(int ClassId, int WeekdayId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var list = db.Schedules.Where(b => b.ClassId == ClassId && b.DayId == WeekdayId).Include("Teacher").OrderBy(o => o.Hour).ToList();
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
