 using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class AuctionController : Controller
	{
		public IActionResult Auction()
		{
			return View("~/Views/Active/Auction.cshtml");
		}
	}
}
