using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Order;

public class ConfirmedOrderDto
{
	public int Id { get; init; }
	public string CustomerId { get; init; }
	public string OwnerId { get; init; }
	public string? CustomerName { get; set; }
	public OrderStatus Status { get; init; }
	public OrderType Type { get; init; }
	public DateTime CreateDate { get; init; }
	public DateTime CloseDate { get; init; }
	public string Comment { get; init; }
	public decimal CheckAmount { get; init; }
	public string DeliveryAddress { get; init; }
	public string DeliveryUser { get; init; }
	public string DeliveryUserPhone { get; init; }
}
