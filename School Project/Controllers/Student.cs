using Microsoft.AspNetCore.Mvc;
using School_Project.Services;

namespace School_Project.Controllers
{
    public class Student : Controller
    {
        public IActionResult Students(int ClassId)
        {
            var Students = StudentServices.GetStudentsByClass(ClassId).ToList();
            var Classes = ClassServices.GetAllClassesInList();
            ViewBag.Classes = Classes;
            ViewBag.ClassId = ClassId;
            ViewBag.ClassName = ClassServices.GetClassById(ClassId).Class1;
            return View(Students);
        }
        public IActionResult Find(int id)
        {
            var Student = StudentServices.GetStudentById(id);
            return new JsonResult(Student);
        }
        public IActionResult Edit(string FirstName, string LastName, string Username, string Password, int StudentId, int ClassId,int CurrentClassId)
        {
            Models.User t = new();
            t.FirstName = FirstName;
            t.LastName = LastName;
            t.Username = Username;
            t.Password = Password;
            t.Id = StudentId;
            if (ClassId != 0)
            {
                t.ClassId = ClassId;
            } else
            {
                t.ClassId = UserServices.GetUserById(StudentId).ClassId;
            }
            t.Role = "Student";

            StudentServices.Edit(t);
            return RedirectToAction("Students", new { ClassId = CurrentClassId });
        }
        public IActionResult Delete(int id, int CurrentClassId)
        {
            UserServices.DeleteUser(id);
            return RedirectToAction("Students", new { ClassId = CurrentClassId });
        }
    }
}
