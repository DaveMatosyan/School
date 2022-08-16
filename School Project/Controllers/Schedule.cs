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
        public IActionResult WeekTimetable(int? ClassId)
        {
            ViewBag.ClassId = ClassId;
            IPrincipal iPrincipalUser = User;
            int UserId = Convert.ToInt32(User.Identity.Name);
            if (User.IsInRole("Student"))
            {
                ClassId = UserServices.GetUserById(UserId).ClassId;
                ViewBag.ClassId = ClassId;
                var schedules = ScheduleServices.GetScedulesByClassId(ClassId);
                ViewBag.schedules = schedules;
            }
            
            if (User.IsInRole("Teacher"))
            {
                var schedules = ScheduleServices.GetScedulesByTeacherId(UserId);
                ViewBag.schedules = schedules;
            }
            if (User.IsInRole("Principal"))
            {
                var schedules = ScheduleServices.GetScedulesByClassId(ClassId);
                ViewBag.schedules = schedules;
            }
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
                int? temp = UserServices.GetUserById(UserId).ClassId;
                ViewBag.ClassId = ClassId;
                List<Models.Schedule> list = ScheduleServices.getWeekDayLessonsByClassId(temp, index);
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
                ScheduleServices.PostSchedule(Credentials, ClassId, user.Id, user.Profession);
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
