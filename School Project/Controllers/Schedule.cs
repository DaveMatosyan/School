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

        public IActionResult ClassTimetable(int ClassId)
        {
            ViewBag.ClassId = ClassId;
            var schedules = ScheduleServices.GetScedulesByClassId(ClassId);
            ViewBag.schedules = schedules;
            return View();
        }
        public IActionResult Timetable(int index, int ClassId)
        {
            ViewBag.index = index;
            ViewBag.ClassId = ClassId;

            IPrincipal iPrincipalUser = User;
            int UserId = Convert.ToInt32(User.Identity.Name);
            if (User.IsInRole("Student"))
            {
                List<Models.Schedule> list = ScheduleServices.GetTeachersLessonsByWeekday(UserId, index);
                return View(list);
            }
            if (User.IsInRole("Teacher"))
            {
                List<Models.Schedule> list = ScheduleServices.GetTeachersLessonsByWeekday(UserId, index);
                return View(list);
            }
            if (User.IsInRole("Principal"))
            {
                List<Models.Schedule> list = ScheduleServices.GetschedulesByClassId(ClassId, index);
                return View(list);
            }
            return View();
        }
        public IActionResult Create(int ClassId)
        {
            ViewBag.ClassId = ClassId;
            return View();
        }
        [HttpPost]
        public IActionResult Create(int ClassId, School_Project.Models.AddSchedule Credentials)
        {
            User user = UserServices.GetUserByUsername(Credentials.TeacherUsername);
            if (user != null)
            {
                //ScheduleServices.PostSchedule(Credentials, ClassId, user.Id, user.Profession);
            }
            return RedirectToAction("Timetable", new { index = Credentials.DayId, ClassId = ClassId });
        }
        public IActionResult Delete(int id, int index, int ClassId)
        {
            ScheduleServices.DeleteSchedule(id);
            return RedirectToAction("Timetable", new { index = index, ClassId = ClassId });
        }
    }
}
