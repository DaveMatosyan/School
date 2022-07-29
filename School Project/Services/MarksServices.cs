using School_Project.Models;

namespace School_Project.Services
{
    public class MarksServices
    {
        static public int CreateMarks()
        {
            using (var context = new SchoolContext())
            {
                Mark mark = new Mark();
                context.Add(mark);
                context.SaveChanges();
                return mark.Id;
            }
        }
    }
}
