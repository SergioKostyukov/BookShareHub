using System.Linq;
using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
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

			var orderTitles = await _orderService.GetActualOrdersAsync(userId);
			var model = new ContractModel
			{
				OrdersTemplated = orderTitles
										.Where(x => x.Status == Core.Domain.Enums.OrderStatus.Template).ToList(),
				OrdersByMeConfirmed = orderTitles
										.Where(x => x.Status == Core.Domain.Enums.OrderStatus.Confirmed &&
													x.CustomerId == userId).ToList(),
				OrdersToMeConfirmed = orderTitles
										.Where(x => x.Status == Core.Domain.Enums.OrderStatus.Confirmed &&
													x.OwnerId == userId).ToList()
			};

			return View("~/Views/Contracts/Contract.cshtml", model);
		}
	}
}
