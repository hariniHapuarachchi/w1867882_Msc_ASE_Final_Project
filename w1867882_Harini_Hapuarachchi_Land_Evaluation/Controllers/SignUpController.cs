using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Data;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Models;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ApplicationContext _context;
        public SignUpController(ApplicationContext db) { 
            _context = db;
        }

        [HttpGet("[action]")]
        [Route("/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User obj, string confPassword)
        {
            _context.Users.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("Index", "SignIn");
        }
    }
}
