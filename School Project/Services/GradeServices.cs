using School_Project.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace School_Project.Services
{
    public class GradeServices
    {
        public static Grade[] GetGrades(string Title, int ClassId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var grades = db.Grades.Where(b => b.Student.ClassId == ClassId && b.Title == Title).ToArray();
                return grades;
            }
        }
        public static Grade GetGrade(int Id)
        {
            using (SchoolContext db = new SchoolContext())
            {
                var grade = db.Grades.FirstOrDefault(b => b.Id == Id);
                return grade;
            }
        }
        public static void AddGrade(int StudenId, int Grade, int Day, int Hour, string Title)
        {
            Grade grade = new();
            grade.StudentId = StudenId;
            grade.Grade1 = Grade;
            grade.Title = Title;
            grade.Hour = Hour;
            DateTime today = DateTime.Today;
            DateTime dt = new DateTime(today.Year, today.Month, Day);
            grade.Day = dt;

            using (SchoolContext db = new SchoolContext())
            {
                db.Add(grade);
                db.SaveChanges();
            }
        }
        public static dynamic GetGradesByClassId(int ClassId)
        {
            using (SchoolContext db = new SchoolContext())
            {
                User[] list = StudentServices.GetStudentsByClass(ClassId);
                var items = db.Grades.Where(b => Array.Exists(list, x => b.StudentId == x.Id));
                return items;
            }
        }
        public static bool IsTeacherValid(int TeacherId, int Day, int ClassId)
        {
            DateTime now = DateTime.Now;
            DateTime dateTime = new DateTime(now.Year, now.Month, Day);
            int DayIndex = ScheduleServices.GetDayIdByWeekday(dateTime.DayOfWeek.ToString());
            using (SchoolContext db = new SchoolContext())
            {
                var item = db.Schedules.FirstOrDefault(b => b.TeacherId == TeacherId && b.ClassId == ClassId && b.DayId == DayIndex);
                if(item == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
