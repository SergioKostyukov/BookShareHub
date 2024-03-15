﻿using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Core.Domain.Entities;

public class Order
{
	public int Id { get; set; }
	public string CustomerId { get; set; } = string.Empty;
	public string OwnerId { get; set; } = string.Empty;
	public OrderStatus Status { get; set; }
	public OrderType Type { get; set; }
	public DateTime CreateDate { get; set; }
	public DateTime? CloseDate { get; set; }
	public decimal CheckAmount { get; set; }
}