using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Project.Models;
using School_Project.Services;

namespace School_Project.Controllers
{
    [Authorize(Roles = "Principal")]
    public class Principal : Controller
    {



    }

}
