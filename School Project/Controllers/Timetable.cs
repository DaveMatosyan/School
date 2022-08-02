using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project.Services;
using System.Security.Principal;

namespace School_Project.Controllers
{
    public class Timetable : Controller
    {

        [Authorize(Roles = "Student")]
        public IActionResult Student()
        {
            IPrincipal iPrincipalUser = User;
            int userId = Convert.ToInt32(User.Identity.Name);
            School_Project.Models.Timetable timetable = TimetableServices.GetTimeTableByStudentId(userId);
            ViewBag.Timetable = timetable;
            return View();
        }
        [Authorize(Roles = "Teacher")]
        public IActionResult Teacher()
        {
            IPrincipal iPrincipalUser = User;
            int userId = Convert.ToInt32(User.Identity.Name);
            List<string> timetable = TimetableServices.GetTeacherTimetableByTeacherId(userId);
            ViewBag.Timetable = timetable;
            return View();
        }
    }
}
