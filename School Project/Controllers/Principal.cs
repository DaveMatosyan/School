using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project.Models;
using School_Project.Services;

namespace School_Project.Controllers
{
    [Authorize(Roles = "Principal")]
    public class Principal : Controller
    {
        public IActionResult AllClasses()
        {
            List<Class> list = ClassServices.GetAllClassesInList();
            return View(list);
        }
        public IActionResult AllDays(int ClassId)
        {
            ViewBag.ClassId = ClassId;
            List<Class> list = ClassServices.GetAllClassesInList();
            return View(list);
        }
        public IActionResult AddTeacher()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTeacher(User user)
        {
            if (UserServices.isUsernameExist(user.Username))
            {
                return RedirectToAction("AddTeacher");
            }
            if (user.Username != null && user.Password != null && user.FirstName != null && user.LastName != null)
            {
                UserServices.PostTeacher(user);
                return RedirectToAction("Login");
            }
            return View();
        }
    }

}
