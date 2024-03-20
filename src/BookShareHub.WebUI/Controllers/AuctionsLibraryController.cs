using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class AuctionsLibraryController : Controller
	{
		public IActionResult AuctionsLibrary()
		{
			return View("~/Views/Library/AuctionsLibrary.cshtml");
		}
	}
}
