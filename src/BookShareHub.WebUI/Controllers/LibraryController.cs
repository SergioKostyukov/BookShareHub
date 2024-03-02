using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class LibraryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
