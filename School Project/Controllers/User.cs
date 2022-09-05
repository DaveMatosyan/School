using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project.Models;
using School_Project.Services;

namespace School_Project.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            int UserId = Convert.ToInt32(User.Identity.Name);
            User user = UserServices.GetUserById(UserId);
            return View(user);
        }
        [HttpPost]
        public IActionResult Profile(User ChangedUser)
        {

            int UserId = Convert.ToInt32(User.Identity.Name);
            User user = UserServices.GetUserById(UserId);
            User OldUser = UserServices.GetUserById(UserId);
            ChangedUser.Id = UserId;
            if (UserServices.isUsernameExist(ChangedUser.Username) && user.Username != ChangedUser.Username)
            {
                ViewBag.Username = "Username already exists";
            }
            if (ChangedUser.FirstName == null || ChangedUser.LastName == null || ChangedUser.Username == null || ViewBag.Username == "Username already exists")
            {
                if(ChangedUser.FirstName == null)
                {
                    ViewBag.FirstName = "FirstName is required";
                }
                if (ChangedUser.LastName == null)
                {
                    ViewBag.LastName = "LastName is required";
                }
                if (ChangedUser.Username == null)
                {
                    ViewBag.Username = "Username is required";
                } 
                return View(user);
            }
            UserServices.UpdateUser(OldUser, ChangedUser);
            return View(user);
        }

        [Authorize(Roles = "Teacher,Student")]
        public async Task<IActionResult> DeleteAsync()
        {
            int UserId = Convert.ToInt32(User.Identity.Name);
            UserServices.DeleteUser(UserId);
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }

}