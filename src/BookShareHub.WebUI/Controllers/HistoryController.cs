using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class HistoryController : Controller
	{ 
		public IActionResult History()
		{
			return View();
		}
	}
}
