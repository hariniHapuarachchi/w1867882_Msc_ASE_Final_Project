using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Data;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Models;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers
{
    public class SignInController : Controller
    {
        private readonly ApplicationContext _context;
        public static string UserName = "";
        public SignInController(ApplicationContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(User obj)
        {
            foreach (var row in _context.Users)
                {
                string[] rec = new string[2];

                if (row.UserName == obj.UserName && row.Password == obj.Password) {
                    rec[0] = obj.UserName;
                    rec[1] = "TRUE";
                    ViewBag.Message = rec;
					string[] x = new string[2];
                    UserName = row.UserName;
					return RedirectToAction("Index", "Dashboard", new {obj.UserName});
				}
                rec[0] = obj.UserName;
                rec[1] = "FALSE";
                ViewBag.Message = rec;
            }
			
			return RedirectToAction("Index");
		}
    }
}
