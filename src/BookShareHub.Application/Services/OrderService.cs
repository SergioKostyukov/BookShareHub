using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class OrderService(ILogger<OrderService> logger,
								BookShareHubDbContext context,
								IMapper mapper,
								IUserService userService) : IOrderService
	{
		private readonly ILogger<OrderService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;
		private readonly IUserService _userService = userService;

		public async Task<List<ActualOrderTitleDto>> GetActualOrdersAsync(string userId)
		{
			var orders = await _context.Orders
				.Where(b => b.CustomerId == userId &&
							b.Status != Core.Domain.Enums.OrderStatus.Done)
				.ToListAsync();

			var orderTitleList = _mapper.Map<List<ActualOrderTitleDto>>(orders);
			foreach (var order in orderTitleList)
			{
				order.OwnerName = await _userService.GetUserNameByIdAsync(order.OwnerId);
				order.CustomerName = await _userService.GetUserNameByIdAsync(order.CustomerId);
			}

			return orderTitleList;
		}

		public async Task<List<DoneOrderTitleDto>> GetDoneOrdersAsync(string userId)
		{
			var orders = await _context.Orders
				.Where(b => b.CustomerId == userId &&
							b.Status == Core.Domain.Enums.OrderStatus.Done)
				.ToListAsync();

			var orderTitleList = _mapper.Map<List<DoneOrderTitleDto>>(orders);
			foreach (var order in orderTitleList)
			{
				order.OwnerName = await _userService.GetUserNameByIdAsync(order.OwnerId);
				order.CustomerName = await _userService.GetUserNameByIdAsync(order.CustomerId);
			}

			return orderTitleList;
		}

		public async Task<OrderDto> GetOrderDetailsAsync(int orderId)
		{
			var order = await _context.Orders
				.FirstOrDefaultAsync(b => b.Id == orderId);

			return _mapper.Map<OrderDto>(order);
		}

		public async Task<int> CreateOrderAsync(OrderCreateDto request)
		{
			var id = await _context.Orders
				.Where(ol => ol.CustomerId == request.CustomerId && ol.OwnerId == request.OwnerId)
				.Select(ol => ol.Id)
				.FirstOrDefaultAsync();

			// If there is no such basket yet
			if (id == 0)
			{
				id = await CreateOrderRecordAsync(request);
			}

			// Adding book to basket
			await CreateOrderListRecordAsync(request, id);

			return id;
		}

		public async Task DeleteOrderAsync(int orderId)
		{
			var order = await _context.Orders
				.Where(o => o.Id == orderId)
				.FirstOrDefaultAsync() ?? throw new InvalidOperationException("Order not found");

			var orderLists = _context.OrdersLists.Where(ol => ol.OrderId == order.Id);

			_context.OrdersLists.RemoveRange(orderLists);
			_logger.LogInformation("OrderList cleared");

			_context.Orders.Remove(order);
			_logger.LogInformation("Order deleted");

			await _context.SaveChangesAsync();
		}

		public async Task<bool> DeleteBookFromOrderAsync(int bookId, int orderId)
		{
			var orderLine = await _context.OrdersLists
				.Where(o => o.OrderId == orderId && o.BookId == bookId)
				.FirstOrDefaultAsync() ?? throw new InvalidOperationException("Order list element not found");

			_context.OrdersLists.Remove(orderLine);
			_logger.LogInformation("Book deleted from 'OrderList'");

			await _context.SaveChangesAsync();

			if (await _context.OrdersLists.AnyAsync(ol => ol.OrderId == orderId) == false)
			{
				var order = await _context.Orders
				.Where(o => o.Id == orderId)
				.FirstOrDefaultAsync() ?? throw new InvalidOperationException("Order not found");

				_context.Orders.Remove(order);
				_logger.LogInformation("Order deleted");

				await _context.SaveChangesAsync();

				return true;
			}

			return false;
		}

		private async Task<int> CreateOrderRecordAsync(OrderCreateDto request)
		{
			var order = _mapper.Map<Order>(request);
			order.Status = Core.Domain.Enums.OrderStatus.Template;
			order.CreateDate = DateTime.Now;
			order.CheckAmount = 0;

			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			return order.Id;
		}

		private async Task CreateOrderListRecordAsync(OrderCreateDto request, int orderId)
		{
			// If there is no such book in basket yet
			var existingOrderList = await _context.OrdersLists
				.FirstOrDefaultAsync(ol => ol.BookId == request.BookId && ol.OrderId == orderId);

			if (existingOrderList == null)
			{
				var bookQuery = new OrderList
				{
					BookId = request.BookId,
					OrderId = orderId,
				};

				_context.OrdersLists.Add(bookQuery);
				await _context.SaveChangesAsync();

				// Add book price to order amount check
				var order = await _context.Orders.FindAsync(orderId);
				if (order != null)
				{
					order.CheckAmount += request.CheckAmount;
					await _context.SaveChangesAsync();
				}
			}
		}
	}
}
