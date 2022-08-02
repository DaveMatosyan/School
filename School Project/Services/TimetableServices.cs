using School_Project.Models;
using System.Text.RegularExpressions;

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
        static public Timetable GetTimeTableByStudentId(int UserId)
        {
            using (var context = new SchoolContext())
            {
                int? classId = UserServices.GetUserById(UserId).ClassId;
                Timetable TimeTable = context.Timetables
    .Single(b => b.ClassId == classId);
                return TimeTable;
            }
        }
        static public List<string> GetTeacherTimetableByTeacherId(int UserId)
        {
            using (var context = new SchoolContext())
            {
                string? Profession = context.Users.Find(UserId).Profession;
                Profession += "Id";
                var ClassesIds = context.Classes
                    .Where(b => b.MathTeacherId == UserId).ToList();
                string wk = DateTime.Today.DayOfWeek.ToString();
                List<string> result = new();
                foreach (Class c in ClassesIds)
                {
                    Timetable timetable = context.Timetables.Find(c.Id);
                    string todaysLessons = timetable[wk];
                    string lesson = Regex.Split(Profession, @"(?<!^)(?=[A-Z])").ToList()[0];
                    var split = todaysLessons.Split(" ");
                    
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (split[i]==lesson)
                        {
                            result.Add(ClassServices.GetClassById(timetable.ClassId).Class1);
                        }
                    }
                }

                return result;
            }
        }

    }
}
