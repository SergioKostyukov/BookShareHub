﻿using BookShareHub.Core.Domain.Entities;
using BookShareHub.Application.Dto;

namespace BookShareHub.Application.Interfaces
{
	public interface IOrderService
	{
		IEnumerable<Order> GetOrders();

		Task CreateOrder();
	}
}
