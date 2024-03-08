using System.Diagnostics;
using BookShareHub.Application.DTOs;
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
