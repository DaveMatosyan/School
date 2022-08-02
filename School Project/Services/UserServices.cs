using School_Project.Models;
using School_Project.Services;

namespace School_Project.Services
{
    public class UserServices
    {
        static public List<User> GetAll()
        {
            using (var context = new SchoolContext())
            {
                return context.Users.ToList();
            }
        }
        static public void PostStudent(User user)
        {
            using (var context = new SchoolContext())
            {
                user.Password = HashServices.HashPassword(user.Password);
                user.Role = "Student";
                context.Add(user);
                context.SaveChanges();
            }
        }
        static public void PostTeacher(User user)
        {
            using (var context = new SchoolContext())
            {
                user.Password = HashServices.HashPassword(user.Password);
                user.Role = "Teacher";
                context.Add(user);
                context.SaveChanges();
            }
        }
        static public bool isUsernameExist(string username)
        {
            using (var context = new SchoolContext())
            {
                foreach (var user in GetAll())
                {
                    if (user.Username == username)
                    {
                        return true;
                    }
                }
                return false;
            }
        }


        static public User? GetUser(string username, string password)
        {
            using (var context = new SchoolContext())
            {
                foreach (var user in GetAll())
                {
                    if (user.Username == username && HashServices.VerifyPassword(user.Password, password))
                    {
                        return user;
                    }
                }
                return null;
            }
        }
        static public User? GetUserById(int Id)
        {
            using (var context = new SchoolContext())
            {
                User user = context.Users.Find(Id);
                if(user==null)
                {
                    return null;
                }
                return user;
            }
        }
    }
}
