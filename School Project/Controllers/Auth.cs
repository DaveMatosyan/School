using Microsoft.AspNetCore.Mvc;
using School_Project.Models;
using School_Project.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;

namespace School_Project.Controllers
{
    public class Auth : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            var classes = ClassServices.GetAllClasses();
            ViewBag.Clases = classes;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogInAsync(LoginFormModel credentials)
        {
            if (credentials.Username == null || credentials.Password == null)
            {
                return View();

            }

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
            return View();
        }
        [HttpPost]
        public IActionResult Signup(User user)
        {
            if (UserServices.isUsernameExist(user.Username))
            {
                return RedirectToAction("SignUp");
            }
            if (user.Username != null && user.Password != null && user.FirstName != null && user.LastName != null)
            {
                UserServices.PostStudent(user);
                return RedirectToAction("Login");
            }
            return View();
        }
        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
