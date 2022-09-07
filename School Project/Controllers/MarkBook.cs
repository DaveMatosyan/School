using Microsoft.AspNetCore.Mvc;
using School_Project.Services;

namespace School_Project.Controllers
{
    public class MarkBook : Controller
    {
        public IActionResult Grades(string Title, int ClassId)
        {
            ViewBag.Grades = MarkBookServices.GetGrades(Title, ClassId);
            ViewBag.ClassName = ClassServices.GetClassById(ClassId).Class1;
            ViewBag.DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var Students = StudentServices.GetStudentsByClassId(ClassId);

            return View(Students);
        }
    }
}
