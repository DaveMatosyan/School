﻿using Microsoft.EntityFrameworkCore;
using School_Project.Models;
using School_Project.Services;
namespace School_Project.Services
{
    public class TeacherServices
    {
        public static dynamic GetTeachersTodaysLessons(int TeacherId)
        {
            DateTime ClockInfoFromSystem = DateTime.Now;
            int WeekDayIndex = (int)ClockInfoFromSystem.DayOfWeek;
            var teacher = UserServices.GetUserById(TeacherId);
            using (SchoolContext db = new SchoolContext())
            {

                var list = db.Schedules.Where(b => b.TeacherId == TeacherId && b.DayId == WeekDayIndex).Include("Class").OrderBy(o => o.Hour).ToList();
                return list;
            }
        }
        public static dynamic GetTeachers()
        {
            using (SchoolContext db = new SchoolContext())
            {

                var list = db.Users.Where(b => b.Profession != null && b.Profession != "Principal").ToList();
                return list;
            }
        }
        public static void Edit(User Teacher)
        {
            if(Teacher.FirstName==null||Teacher.LastName==null||Teacher.Username==null)
            {
                return;
            }
            var OldTeacher = UserServices.GetUserById(Teacher.Id);
            if (Teacher.Password == null)
            {
                Teacher.Password = OldTeacher.Password;
            } else
            {
                Teacher.Password = HashServices.HashPassword(Teacher.Password);

            }
            using (SchoolContext db = new SchoolContext())
            {
                db.Update(Teacher);
                db.SaveChanges();
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

        public static List<int?> TeachersClassesIds(int TeacherId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var list = db.Schedules.Where(b => b.TeacherId == TeacherId).Include("Class").ToArray();
                List<int?> result = new List<int?>();
                foreach(var item in list)
                {
                    if(!result.Contains(item.ClassId))
                    {
                        result.Add(item.ClassId);
                    }
                }
                return result;
            }
        }
        public static bool IsTeacherBusy(int TeacherId, int DayId, int Hour)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var item = db.Schedules.FirstOrDefault(x => x.TeacherId == TeacherId && x.DayId == DayId && x.Hour == Hour);
                if(item != null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
