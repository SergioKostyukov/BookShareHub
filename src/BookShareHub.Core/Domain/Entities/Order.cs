﻿using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Core.Domain.Entities;

public class Order
{
	public int Id { get; set; }
	public int CustomerId { get; set; }
	public int OwnerId { get; set; }
	public OrderStatus OrderStatus { get; set; }
	public OrderType OrderType { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime OrderDate { get; set; }
	public decimal CheckAmount { get; set; }
}