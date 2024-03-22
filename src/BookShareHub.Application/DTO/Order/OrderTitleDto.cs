using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Order;

public class DoneOrderTitleDto
{
    public int Id { get; init; }
    public string CustomerId { get; init; }
    public string OwnerId { get; init; }
    public string? CustomerName { get; set; }
    public string? OwnerName { get; set; }
    public DateTime CloseDate { get; init; }
	public OrderStatus Status { get; init; }
}

public class ActualOrderTitleDto
{
    public int Id { get; init; }
    public string CustomerId { get; init; }
    public string OwnerId { get; init; }
    public string CustomerName { get; set; }
    public string OwnerName { get; set; }
    public OrderStatus Status { get; init; }
	public OrderType Type { get; init; }
    public DateTime CreateDate { get; init; }
}

public class ActualTemplatedOrderDto
{
	public int Id { get; init; }
	public string OwnerId { get; init; }
	public OrderStatus Status { get; init; }
	public OrderType Type { get; init; }
	public DateTime CreateDate { get; init; }
}