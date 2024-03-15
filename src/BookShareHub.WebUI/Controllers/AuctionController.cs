using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class AuctionController : Controller
	{
		public IActionResult Auction()
		{
			return View("~/Views/Active/Auction.cshtml");
		}
	}
}
