using BookShareHub.Core.Domain.Enums;

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
	public string Comment { get; set; } = string.Empty;
	public decimal CheckAmount { get; set; }
	public string DeliveryAddress { get; set; } = string.Empty;
	public string DeliveryUser { get; set; } = string.Empty;
	public string DeliveryUserPhone { get; set; } = string.Empty;
}