using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto;

public class OrderDto
{
	public int Id { get; set; }
	public string CustomerId { get; set; }
	public string OwnerId { get; set; }
	public OrderStatus Status { get; set; }
	public OrderType Type { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime CloseDate { get; set; }
	public decimal CheckAmount { get; set; }
}

public record OrderCreateDto (
	string CustomerId,
	string OwnerId,
	int BookId,
	OrderType? Type,
	decimal CheckAmount
);