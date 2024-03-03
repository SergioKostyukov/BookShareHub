using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
    [Authorize]
    public class LibraryController(ILogger<HomeController> logger) : Controller
	{
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Library()
		{
			return View();
		}
	}
}
