using Microsoft.AspNetCore.Mvc;
using School_Project.Models;
using School_Project.Services;

namespace School_Project.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Edit()
        {
            int UserId = Convert.ToInt32(User.Identity.Name);
            User user = UserServices.GetUserById(UserId);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User ChangedUser)
        {
            if (ChangedUser.FirstName == null || ChangedUser.LastName == null || ChangedUser.Username == null)
            {
                return View();
            }
            int UserId = Convert.ToInt32(User.Identity.Name);
            User OldUser = UserServices.GetUserById(UserId);
            ChangedUser.Id = UserId;
            UserServices.UpdateUser(OldUser, ChangedUser);
            return Redirect("/");
        }
    }

}
