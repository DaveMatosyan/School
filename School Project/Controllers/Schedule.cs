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
        [Authorize(Roles = "Principal")]
        public IActionResult Create(int ClassId, int DayId, int Hour)
        {
            ViewBag.ClassId = ClassId;
            ViewBag.DayId = DayId;
            ViewBag.Hour = Hour;

            ViewBag.Teachers = TeacherServices.GetTeachers();

            return View();
        }
        [Authorize(Roles = "Principal")]
        [HttpPost]
        public IActionResult Create(int ClassId, School_Project.Models.AddSchedule Credentials)
        {
            int test = ClassId;
            User user = UserServices.GetUserByUsername(Credentials.TeacherUsername);
            if (user != null)
            {
                ScheduleServices.PostSchedule(Credentials, ClassId, user.Id, user.Profession);
            }
            return RedirectToAction("WeekTimetable", new { ClassId = ClassId });
        }
        [Authorize(Roles = "Principal")]
        public IActionResult Edit(int DayId, int Hour, int ClassId)
        {
            ViewBag.ClassId = ClassId;
            ViewBag.Teachers = TeacherServices.GetTeachers();
            Models.Schedule schedule = ScheduleServices.GetScedule(ClassId, DayId, Hour);
            return View(schedule);
        }
        [Authorize(Roles = "Principal")]
        [HttpPost]
        public IActionResult Edit(int ClassId, int SId, string STitle, Models.Schedule schedule)
        {
            schedule.Id = SId;
            string Profession = UserServices.GetUserById(schedule.TeacherId).Profession;
            schedule.Title = Profession + "Hour";
            ScheduleServices.UpdateSchedule(schedule);
            return RedirectToAction("WeekTimetable", new { ClassId = ClassId });

        }
        [Authorize(Roles = "Principal")]
        public IActionResult Delete(int id, int index, int ClassId)
        {
            ScheduleServices.DeleteSchedule(id);
            return RedirectToAction("Timetable", new { index = index, ClassId = ClassId });
        }

    }
}
