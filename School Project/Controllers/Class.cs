using Microsoft.AspNetCore.Mvc;
using School_Project.Services;
using School_Project.Models;

namespace School_Project.Controllers
{
    public class Class : Controller
    {
        public IActionResult AddClass()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddClass(School_Project.Models.Class c)
        {
            if (ClassServices.IsClassExist(c.Class1))
            {
                return RedirectToAction("AddTeacher");
            }
            if (c.Class1 != null)
            {
                ClassServices.PostClass(c);
                return RedirectToAction("AllClasses");
            }
            return View();
        }
        public IActionResult AllClasses()
        {
            List<School_Project.Models.Class> list = ClassServices.GetAllClassesInList();
            return View(list);
        }
    }
}
