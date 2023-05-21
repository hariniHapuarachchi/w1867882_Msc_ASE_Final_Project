using Microsoft.AspNetCore.Mvc;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers
{
    public class DashboardController : Controller
    {
        public static string UserName = "";
        public IActionResult Index(string userName)
        {
            UserName = userName;
            return View();
        }

        public IActionResult Evaluate()
        {
            return RedirectToAction("Index", "MenuCrop", new { UserName });
        }
        public IActionResult UserManagement()
        {
            return RedirectToAction("Index", "UserManagement", new { UserName });
        }
    }
}
