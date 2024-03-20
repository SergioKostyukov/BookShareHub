using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class AuctionController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
