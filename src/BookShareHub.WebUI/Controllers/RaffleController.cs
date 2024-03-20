using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class RaffleController : Controller
	{
		public IActionResult Raffle()
		{
			return View("~/Views/Raffle/Raffle.cshtml");
		}
	}
}
