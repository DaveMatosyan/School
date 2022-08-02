using School_Project.Models;
namespace School_Project.Services
{
    public class TimetableServices
    {
        static public List<Timetable> GetAll()
        {
            using (var context = new SchoolContext())
            {
                return context.Timetables.ToList();
            }
        }
    //    static public Timetable GetTimeTableByClass(string Class)
    //    {
    //        using (var context = new SchoolContext())
    //        {
    //            Timetable TimeTable = context.Timetables
    //.Single(b => b.ClassName == Class);
    //            return TimeTable;
    //        }
    //    }
        static public List<string> GetAllClasses()
        {
            using (var context = new SchoolContext())
            {
                var items = context.Classes.Where(u => u.Class1 != null).Select(u => u.Class1).ToList();
                return items;

            }
        }
    }
}
