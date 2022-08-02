using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project.Services;

namespace School_Project.Controllers
{
    public class Timetable : Controller
    {

        //[Authorize(Roles = "Student")]
        //public IActionResult Show()
        //{
        //    //var value = Request.Cookies[]
        //    Timetable timetable = TimetableServices.GetTimeTableByClass();
        //    ViewBag.Timetables = timetable;
        //    return View();
        //}

    }
}
