using School_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace School_Project.Services
{
    public class ScheduleServices
    {
        public static Schedule GetScedule(int? ClassId, int DayId, int Hour)
        {
            using (SchoolContext db = new SchoolContext())
            {
                Schedule schedule = db.Schedules.Where(b => b.ClassId == ClassId && b.DayId == DayId && b.Hour == Hour).Include("Teacher").FirstOrDefault<Schedule>(); ;
                return schedule;
            }
        }
        public static List<Schedule> GetScedulesByClassId(int? ClassId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var list = db.Schedules.Where(b => b.ClassId == ClassId).OrderBy(o => o.Hour).OrderBy(o => o.DayId).ToList();
                return list;
            }
        }
        public static List<Schedule> GetScedulesByTeacherId(int? TeacherId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var list = db.Schedules.Where(b => b.TeacherId == TeacherId).Include("Class").OrderBy(o => o.Hour).OrderBy(o => o.DayId).ToList();
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
                var IsExsist = db.Schedules.FirstOrDefault(c => c.ClassId == schedule.ClassId && c.DayId == schedule.DayId && c.Hour == schedule.Hour);
                if(IsExsist == null)
                {
                    db.Add(schedule);
                    db.SaveChanges();
                }

            }
        }
        
        public static void UpdateSchedule(Schedule schedule)
        {
            using (SchoolContext db = new SchoolContext())
            {
                db.Update(schedule);
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
        public static dynamic getWeekDayLessonsByClassId(int ?ClassId, int WeekdayId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var list = db.Schedules.Where(b => b.ClassId == ClassId && b.DayId == WeekdayId).Include("Teacher").OrderBy(o => o.Hour).ToList();
                return list;
            }
        }
        public static string GetWeekdayByDayId(int DayId)
        {
            switch (DayId)
            {

                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                case 6:
                    return "Saturday";
                case 7:
                    return "Sunday";
            }
            return "sss";
        }
    }
}
