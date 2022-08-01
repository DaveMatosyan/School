using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project.Services;

namespace School_Project.Controllers
{
    public class Timetable : Controller
    {

        [Authorize]
        public IActionResult Show()
        {
            List<School_Project.Models.Timetable> timetables = TimetableServices.GetAll();
            ViewBag.Timetables = timetables;
            return View();
        }

    }
}
