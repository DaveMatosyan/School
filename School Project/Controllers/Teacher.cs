using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project.Models;
using School_Project.Services;

namespace School_Project.Controllers
{
    [Authorize(Roles = "Principal")]
    public class Teacher : Controller
    {
        public IActionResult AllTeachers()
        {
            List<User> list = TeacherServices.GetTeachers();
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
        public IActionResult Edit(int id)
        {
            var Teacher = UserServices.GetUserById(id);
            return View(Teacher);
        }
        [HttpPost]
        public IActionResult Edit(User Teacher)
        {
            TeacherServices.Edit(Teacher);
            return RedirectToAction("AllTeachers");
        }
        public IActionResult Details(int id)
        {
            var Teacher = UserServices.GetUserById(id);
            return View(Teacher);
        }
        public IActionResult Delete(int id)
        {
            TeacherServices.DeleteTeacher(id);
            return RedirectToAction("AllTeachers");
        }
    }
}
