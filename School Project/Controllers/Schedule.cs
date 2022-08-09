using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project.Models;
using School_Project.Services;
using System.Security.Principal;

namespace School_Project.Controllers
{
    [Authorize]
    public class Schedule : Controller
    {
        [Authorize(Roles = "Student,Teacher")]
        public IActionResult AllDays()
        {
            return View();
        }

        public IActionResult Timetable(int index, int ClassId)
        {

            IPrincipal iPrincipalUser = User;
            int UserId = Convert.ToInt32(User.Identity.Name);
            if (User.IsInRole("Student"))
            {
                List<Models.Schedule> list = StudentServices.GetTeachersLessonsByWeekday(UserId, index);
                return View(list);
            }
            if (User.IsInRole("Teacher"))
            {
                List<Models.Schedule> list = TeacherServices.GetTeachersLessonsByWeekday(UserId, index);
                return View(list);
            }
            if (User.IsInRole("Principal"))
            {
                List<Models.Schedule> list = PrincipalServices.GetschedulesByClassId(ClassId, index);
                return View(list);
            }
            return View();
        }

    }
}
