namespace BookShareHub.Core.Domain.Entities;

public enum OrderStatus
{
	Request,
	Agreed,
	Done,
	Canceled
}

public enum OrderType
{
	Free,
	Trade,
	Sale,
	Raffle,
	Auction
}

public class Order
{
	public int Id { get; set; }
	public int CustomerId { get; set; }
	public int OwnerId { get; set; }
	public OrderStatus OrderStatus { get; set; }
	public OrderType OrderType { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime OrderDate { get; set; }
	public decimal Check { get; set; } // check amount
}