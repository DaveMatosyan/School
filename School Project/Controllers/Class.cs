
using Microsoft.AspNetCore.Mvc;
using School_Project.Services;
using School_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace School_Project.Controllers
{
    public class Class : Controller
    {
        [Authorize(Roles = "Principal")]
        public IActionResult AddClass()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Principal")]
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
        [Authorize(Roles = "Principal,Teacher")]
        public IActionResult AllClasses()
        {
            List<School_Project.Models.Class> list = new();
            if (User.IsInRole("Principal")) {
                list = ClassServices.GetAllClassesInList();

            } else
            {
                int UserId = Convert.ToInt32(User.Identity.Name);

                list = ClassServices.GetClassesByClassIds(UserId);

            }

            return View(list);
        }
        [Authorize(Roles = "Principal")]
        public IActionResult Edit(int Id)
        {
            ViewBag.ClassId = Id;
            Models.Class c = ClassServices.GetClassById(Id);
            return View(c);
        }
        [HttpPost]
        [Authorize(Roles = "Principal")]
        public IActionResult Edit(Models.Class c)
        {
            ClassServices.UpdateClass(c);
            return RedirectToAction("AllClasses");
        }
        [Authorize(Roles = "Principal")]
        public IActionResult Delete(int Id)
        {
            ClassServices.DeleteClass(Id);
            return RedirectToAction("AllClasses");
        }
    }
}
