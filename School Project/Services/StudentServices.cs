using School_Project.Models;
using System.Linq;

namespace School_Project.Services
{
    public class StudentServices
    {
        static public List<User> GetAll()
        {
            using (var context = new SchoolContext())
            {
                var query = context.Users
                                   .Where(s => s.Role == "Student");
                return query.ToList();
            }
        }
    }
}
