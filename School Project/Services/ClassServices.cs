using School_Project.Models;

namespace School_Project.Services
{
    public class ClassServices
    {
        static public List<string> GetAllClasses()
        {
            using (var context = new SchoolContext())
            {
                var items = context.Classes.Where(u => u.Class1 != null).Select(u => u.Class1).ToList();
                return items;

            }
        }
        static public Class? GetClassById(int Id)
        {
            using (var context = new SchoolContext())
            {
                Class class1 = context.Classes.Find(Id);
                if (class1 == null)
                {
                    return null;
                }
                return class1;
            }
        }
    }
}
