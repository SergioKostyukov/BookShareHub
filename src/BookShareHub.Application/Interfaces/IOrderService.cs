using BookShareHub.Core.Domain.Entities;
using BookShareHub.Application.DTOs;

namespace BookShareHub.Application.Interfaces
{
	public interface IOrderService
	{
		IQueryable<Order> GetOrders();


	}
}
