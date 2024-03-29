﻿using System.Security.Claims;
using BookShareHub.Application.Interfaces;
using BookShareHub.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShareHub.WebUI.Controllers
{
	[Authorize]
	public class HistoryController(ILogger<BookController> logger,
								   IHttpContextAccessor httpContextAccessor,
								   IBooksLibraryService libraryService,
								   IOrderService orderService,
								   IUserService userService) : Controller
	{
		private readonly ILogger<BookController> _logger = logger;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly IBooksLibraryService _libraryService = libraryService;
		private readonly IOrderService _orderService = orderService;
		private readonly IUserService _userService = userService;

		[HttpGet]
		public async Task<IActionResult> History()
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var model = new HistoryModel
			{
				OrderTitles = await _orderService.GetDoneOrdersAsync(userId)
			};

			return View("~/Views/Contract/History.cshtml", model);
		}

		[HttpGet]
		public async Task<IActionResult> GetOrderDetails(int orderId)
		{
			string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				return BadRequest("UserId not found");
			}

			var orderDetails = await _orderService.GetOrderDetailsAsync(orderId);
			if (orderDetails == null)
			{
				return NotFound();
			}

			var ownerInfo = await _userService.GetUserByIdAsync(orderDetails.OwnerId);
			if (ownerInfo == null)
			{
				return NotFound();
			}

			var model = new HistoryModel
			{
				Order = orderDetails,
				User = ownerInfo,
				OrderList = await _libraryService.GetAllBooksByOrderIdAsync(orderId)
			};

			return Json(model);
		}
	}
}
