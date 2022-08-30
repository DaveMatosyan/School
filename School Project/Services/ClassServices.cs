using School_Project.Models;

namespace School_Project.Services
{
    public class ClassServices
    {
        static public List<Class> GetAllClasses()
        {
            using (var context = new SchoolContext())
            {
                var items = context.Classes.ToList();
                return items;

            }
        }
        static public List<Class> GetAllClassesInList()
        {
            using (var context = new SchoolContext())
            {
                var items = context.Classes.ToList();
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
        static public void PostClass(Class c)
        {
            using (var context = new SchoolContext())
            {
                context.Add(c);
                context.SaveChanges();
            }
        }
        
        static public bool IsClassExist(string ClassName)
        {
            using (var context = new SchoolContext())
            {
                foreach (var c in GetAllClasses())
                {
                    if (c.Class1 == ClassName)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static void DeleteClass(int ClassId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var itemsToRemove = db.Schedules.Where(x => x.ClassId == ClassId); //returns a single item.
                var Class = db.Classes.Find(ClassId);
                db.Schedules.RemoveRange(itemsToRemove);
                db.Classes.Remove(Class);
                db.SaveChanges();
            }
        }
        public static void UpdateClass(Class c)
        {
            if(c == null)
            {
                return;
            }
            using (SchoolContext db = new SchoolContext())
            {
                db.Update(c);
                db.SaveChanges();

            }
        }
    }
}
