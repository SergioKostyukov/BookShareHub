using System.Diagnostics;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
        public IActionResult Index()
		{
			return View();
		}
	}
}
