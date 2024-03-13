using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	public class ContractController(ILogger<ContractController> logger, 
								    IHttpContextAccessor httpContextAccessor, 
									IOrderService orderService) : Controller
	{
		private readonly ILogger<ContractController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IOrderService _orderService = orderService;
		
		[HttpGet]
		public async Task<IActionResult> Contract()
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var model = new ContractModel
			{
				OrderTitles = await _orderService.GetActualOrdersAsync(userId),
			};

			return View("~/Views/Contracts/Contract.cshtml", model);
		}
	}
}
