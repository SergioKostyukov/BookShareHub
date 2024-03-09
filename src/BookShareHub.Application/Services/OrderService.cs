using BookShareHub.Application.DTOs;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShareHub.Application.Services
{
	internal class OrderService(BookShareHubDbContext context) : IOrderService
	{
		private readonly BookShareHubDbContext _context = context;

		public IQueryable<Order> GetOrders()
		{
			throw new NotImplementedException();
		}

		// ----------------------- GET METHODS -----------------------


		// ----------------------- PATCH METHODS -----------------------


	}
}
