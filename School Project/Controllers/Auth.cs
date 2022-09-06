using Microsoft.AspNetCore.Mvc;
using School_Project.Models;
using School_Project.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;

namespace School_Project.Controllers
{
    public class Auth : Controller
    {
        public IActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogInAsync(LoginFormModel credentials)
        {

            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            if (credentials.Username == null || credentials.Password == null)
            {
                return View();

            }
            if (ModelState.IsValid)
            {
                User user = UserServices.GetUser(credentials.Username, credentials.Password);
                if (user != null)
                {

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                };
                    var idenity = new ClaimsIdentity(claims, "MyCookieAuth");

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(idenity);
                    await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(credentials.Username), null);
                    return Redirect("/");
                }
                else
                {
                    ViewBag.Wrong = "username or password is incorrect";
                }
            }


            return View();
        }
        public IActionResult SignUp(bool UserExist)
        {

            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            var classes = ClassServices.GetAllClasses();
            ViewBag.Clases = classes;
            if(UserExist)
            {
                ViewBag.UserExist = true;
            }
            else
            {
                ViewBag.UserExist = false;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Signup(SignupFormModel SFM)
        {
            Models.User user = new Models.User();
            user.FirstName = SFM.FirstName;
            user.LastName = SFM.LastName;
            user.Username = SFM.Username;
            user.Password = SFM.Password;
            user.ClassId = SFM.ClassId;
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            bool UserExist = UserServices.isUsernameExist(user.Username);
            if (ModelState.IsValid)
            {

                if (UserExist)
                {
                     
                    return RedirectToAction("SignUp", true);
                }
                if (user.Username != null && user.Password != null && user.FirstName != null && user.LastName != null)
                {
                    UserServices.PostStudent(user);
                    return RedirectToAction("Login");
                }
            }
            var classes = ClassServices.GetAllClasses();
            ViewBag.Clases = classes;
            ViewBag.UserExist = UserExist;
            return View();

        }
        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
