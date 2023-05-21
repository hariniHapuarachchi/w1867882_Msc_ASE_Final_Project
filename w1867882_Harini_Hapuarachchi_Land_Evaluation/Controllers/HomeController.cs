using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Models;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SignOut()
        {
            w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers.SignInController.UserName = "";
            return RedirectToAction("Index", "Home");
        }
        
    }
}