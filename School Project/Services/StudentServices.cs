using Microsoft.EntityFrameworkCore;
using School_Project.Models;

namespace School_Project.Services
{
    public class StudentServices
    {

        public static dynamic GetStudentsByClassId(int ClassId)
        {
            using (SchoolContext db = new SchoolContext())
            {

                var items = db.Users.Where(b => b.ClassId == ClassId).OrderBy(x => x.Id).ToArray();
                return items;
            }
        }

    }
}
