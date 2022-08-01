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
    }
}
