namespace BookShareHub.Core.Domain.Entities;

public class Order
{
	public int Id { get; set; }
	public int CustomerId { get; set; }
	public int OwnerId { get; set; }
	public int OrderStatus { get; set; } // request/agreed/done/canseled
	public int OrderType { get; set; } // free/trade/sale/raffle/auction
	public DateTime CreatedDate { get; set; }
	public DateTime OrderDate { get; set; }
	public float Check { get; set; } // check amount
}