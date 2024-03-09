using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class ChatController : Controller
    {
        public IActionResult Chat()
        {
            return View();
        }
    }
}
