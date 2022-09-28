using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using School_Project.Models;
using School_Project.Services;

namespace School_Project.Controllers
{
    public class MarkBook : Controller
    {
        [Authorize(Roles = "Teacher")]
        public IActionResult Grades(string Title, int ClassId)
        {
            int TeacherId = Convert.ToInt32(User.Identity.Name);
            string str = UserServices.GetUserById(TeacherId).Profession + "Hour";
            ViewBag.Grades = MarkBookServices.GetGrades(str, ClassId);
            ViewBag.Class = ClassServices.GetClassById(ClassId);
            ViewBag.DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            ViewBag.Schedules = ScheduleServices.GetScedulesByClassIdAndLesson(ClassId, str).ToArray();

            ViewBag.c = str;
            var Students = StudentServices.GetStudentsByTeacherAndClass(TeacherId, ClassId);

            return View(Students);
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult Find(int id)
        {
            var Grade = MarkBookServices.GetGrade(id);
            return new JsonResult(Grade);
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult Create(int ClassId, int Day)
        {
            int TeacherId = Convert.ToInt32(User.Identity.Name);
            Bool IsValid = new();
            if(GradeServices.IsTeacherValid(TeacherId,Day,ClassId))
            {
                IsValid.IsValid = true;
            } 
            else
            {
                IsValid.IsValid = false;
            }

            return new JsonResult(IsValid);
        }
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult Form(int ItemId, int StudentId, int Grade, int DayId, int ClassId)
        {
            int TeacherId = Convert.ToInt32(User.Identity.Name);

            string Title = UserServices.GetUserById(TeacherId).Profession + "Hour";
            if (Grade < 1 || Grade > 10 )
            {
                return RedirectToAction("Grades", new { ClassId = ClassId, Title = Title });
            }
            if(ItemId == 0)
            {
                MarkBookServices.AddGrade(StudentId, TeacherId, DayId, ClassId, Title, Grade);
                return RedirectToAction("Grades", new { ClassId = ClassId, Title = Title });

            }
            MarkBookServices.EditGrade(Grade, ItemId);
            return RedirectToAction("Grades", new { ClassId = ClassId, Title = Title });
        }
        [Authorize(Roles = "Student")]
        public IActionResult Marks(int Month, int Year)
        {
            int StudentId = Convert.ToInt32(User.Identity.Name);

            if(Year == 0)
            {
                Year = DateTime.Now.Year;
                Month = DateTime.Now.Month;
                ViewBag.DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            } else
            {
                ViewBag.DaysInMonth = DateTime.DaysInMonth(Year, Month);

            }
            ViewBag.Year = Year;
            ViewBag.Month = Month;
            var Grades = MarkBookServices.GetGradesByMonthYearStudent(Month, Year, StudentId);

            return View(Grades);
        }
    }
}
