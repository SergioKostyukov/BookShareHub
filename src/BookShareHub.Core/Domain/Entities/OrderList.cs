namespace BookShareHub.Core.Domain.Entities;

public class OrderList
{
	public int Id { get; set; }
	public int OrderId { get; set; }
	public int UserID { get; set; }
	public int BookId { get; set; }
}
