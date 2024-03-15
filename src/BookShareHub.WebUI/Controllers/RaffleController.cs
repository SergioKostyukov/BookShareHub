using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class RaffleController : Controller
	{
		public IActionResult Raffle()
		{
			return View("~/Views/Active/Raffle.cshtml");
		}
	}
}
