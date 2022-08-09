using School_Project.Models;

namespace School_Project.Services
{
    public class PrincipalServices
    {
        
        public static dynamic GetschedulesByClassId(int ClassId, int WeekdayId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var list = db.Schedules.Where(b => b.ClassId == ClassId && b.DayId == WeekdayId).OrderBy(o => o.Hour).ToList();
                return list;
            }
        }
    }
}
