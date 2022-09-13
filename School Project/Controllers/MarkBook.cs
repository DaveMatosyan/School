using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project.Services;

namespace School_Project.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class MarkBook : Controller
    {
        public IActionResult Grades(string Title, int ClassId)
        {
            ViewBag.Grades = MarkBookServices.GetGrades(Title, ClassId);
            ViewBag.Class = ClassServices.GetClassById(ClassId);
            ViewBag.DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            int TeacherId = Convert.ToInt32(User.Identity.Name);
            var Students = StudentServices.GetStudentsByTeacher(TeacherId);

            return View(Students);
        }
        public IActionResult Find(int id)
        {
            var Grade = MarkBookServices.GetGrade(id);
            return new JsonResult(Grade);
        }
        [HttpPost]
        public IActionResult Form(int ItemId, int StudentId, int Grade, int Day, int Hour, int ClassId)
        {
            int TeacherId = Convert.ToInt32(User.Identity.Name);

            string Title = UserServices.GetUserById(TeacherId).Profession + "Hour";
            if (Grade < 1 && Grade > 10 || Day < 1 || Hour < 1 && Hour > 7)
            {
                return RedirectToAction("Grades", new { ClassId = ClassId, Title = Title });
            }
            if(ItemId == 0)
            {
                MarkBookServices.AddGrade(StudentId, Grade, Day, Hour, Title);
                return RedirectToAction("Grades", new { ClassId = ClassId, Title = Title });

            }
            //MarkBookServices.EditGrade(int StudenId, int Grade, int Day);
            return RedirectToAction("Grades", new { ClassId = ClassId, Title = Title });
        }
    }
}
