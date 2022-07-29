using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project.Models;
using School_Project.Services;

namespace School_Project.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class Student : Controller
    {
        public IActionResult AllStudents()
        {
            List<User> Students = StudentServices.GetAll();
            ViewBag.Students = Students;
            return View();
        }
    }
}
