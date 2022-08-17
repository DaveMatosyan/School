using Microsoft.AspNetCore.Mvc;
using School_Project.Models;
using School_Project.Services;
using System.Diagnostics;

namespace School_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int UserId = Convert.ToInt32(User.Identity.Name);
            ViewBag.User = UserServices.GetUserById(UserId);
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}