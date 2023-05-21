using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Data;
using w1867882_Harini_Hapuarachchi_Land_Evaluation.Models;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationContext _context;
        public UserManagementController(ApplicationContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            var users = _context.Users
                    .Where(user => user.UserName == w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers.SignInController.UserName)
                    .FirstOrDefault();
            List<string> userList = new List<string>{users.UserId.ToString(), users.UserName, users.FirstName, users.LastName, users.Password,
                users.Address, users.Gender.ToString(),users.Phone.ToString(),users.Email,users.Dob.ToString(),users.Nic };
                ViewBag.LoggedUser = userList.ToArray();
            return View();
        }

        public IActionResult UpdateUser(User obj)
        {
            _context.Users.Update(obj);
            _context.SaveChanges();
            return RedirectToAction("Index", "UserManagement");
        }
    }
}
