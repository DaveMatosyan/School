using Microsoft.AspNetCore.Mvc;
using School_Project.Services;
using School_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace School_Project.Controllers
{
    [Authorize(Roles = "Principal")]
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
        public IActionResult Edit(int Id)
        {
            ViewBag.ClassId = Id;
            Models.Class c = ClassServices.GetClassById(Id);
            return View(c);
        }
        [HttpPost]
        public IActionResult Edit(Models.Class c)
        {
            ClassServices.UpdateClass(c);
            return RedirectToAction("AllClasses");
        }
        public IActionResult Delete(int Id)
        {
            ClassServices.DeleteClass(Id);
            return RedirectToAction("AllClasses");
        }
    }
}
