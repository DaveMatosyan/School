﻿using School_Project.Models;
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
        static public User GetUserById(int Id)
        {
            using (var context = new SchoolContext())
            {
                User user = context.Users.Find(Id);
                return user;
            }
        }
        static public User GetUserByUsername(string username)
        {
            using (var context = new SchoolContext())
            {
                User user = context.Users.FirstOrDefault(b => b.Username == username);
                return user;
            }
        }
        public static void UpdateUser(User OldUser, User ChangedUser)
        {
            ChangedUser.Id = OldUser.Id;
            ChangedUser.Role = OldUser.Role;

            if (ChangedUser.Password != null)
            {
                ChangedUser.Password = HashServices.HashPassword(ChangedUser.Password);
            }
            else
            {
                ChangedUser.Password = OldUser.Password;
            }
            if (OldUser.Profession == "Principal")
            {
                ChangedUser.Profession = "Principal";
            }

            using (SchoolContext db = new SchoolContext())
            {
                db.Update(ChangedUser);
                db.SaveChanges();

            }
        }

        public static void DeleteUser(int Id)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var itemToRemove = db.Users.SingleOrDefault(x => x.Id == Id); //returns a single item.

                if (itemToRemove != null)
                {
                    db.Users.Remove(itemToRemove);
                    db.SaveChanges();
                }
            }
        }
    }
}
