using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
    public class ActiveController : Controller
    {
        public IActionResult Active()
        {
            return View();
        }
    }
}
