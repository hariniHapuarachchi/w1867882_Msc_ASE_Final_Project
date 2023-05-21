using Microsoft.AspNetCore.Mvc;

namespace w1867882_Harini_Hapuarachchi_Land_Evaluation.Controllers
{
	public class MenuCropController : Controller
	{
		public static string UsersName = "";
		public IActionResult Index(string userName)
		{
			UsersName = userName;
			return View();
		}

		public IActionResult EvaluateTCrop() {
			return RedirectToAction("Index", "Evaluate", new {UsersName});
		}

		public IActionResult EvaluateRCrop() {
			return RedirectToAction("Index", "EvaluateRubber", new { UsersName });
		}

		public IActionResult EvaluateCCrop() {
			return RedirectToAction("Index", "EvaluateCoconut", new { UsersName });
		}
	}
}
