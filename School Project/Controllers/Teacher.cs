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
                ViewBag.Username = "Username already exists";
            }
            if (user.Username == null || user.Password == null || user.FirstName == null || user.LastName == null || ViewBag.Username == "Username already exists")
            {
                if (user.Username == null)
                {
                    ViewBag.Username = "Username is required";
                }
                if (user.Password == null)
                {
                    ViewBag.Password = "Password is required";
                }
                if (user.FirstName == null)
                {
                    ViewBag.FirstName = "FirstName is required";
                }
                if (user.LastName == null)
                {
                    ViewBag.LastName = "LastName is required";
                }
                return View();

            }




            TeacherServices.PostTeacher(user);
            return RedirectToAction("AllTeachers");

        }

        public IActionResult Edit(int id)
        {
            var Teacher = UserServices.GetUserById(id);
            return View(Teacher);
        }
        [HttpPost]
        public IActionResult Edit(string FirstName, string LastName, string Username, string Password, int TeacherId, string Profession)
        {
            Models.User t = new();
            t.FirstName = FirstName;
            t.LastName = LastName;
            t.Username = Username;
            t.Password = Password;
            t.Id = TeacherId;
            t.Profession = Profession;
            t.Role = "Teacher";

            TeacherServices.Edit(t);
            return RedirectToAction("AllTeachers");
        }
        public IActionResult Find(int id)
        {
            var Teacher = UserServices.GetUserById(id);
            return new JsonResult(Teacher);
        }
        public IActionResult Delete(int id)
        {
            UserServices.DeleteUser(id);
            return RedirectToAction("AllTeachers");
        }
    }
}
