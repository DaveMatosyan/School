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
        static public List<string> GetAllClasses()
        {
            using (var context = new SchoolContext())
            {
                var items = context.Timetables.Where(u => u.ClassName != null).Select(u => u.ClassName).ToList();
                return items;

            }
        }
    }
}
