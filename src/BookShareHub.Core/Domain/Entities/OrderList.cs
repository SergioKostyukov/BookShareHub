namespace BookShareHub.Core.Domain.Entities;

internal class OrderList
{
	public int Id { get; set; }
	public int OrderId { get; set; }
	public int UserID { get; set; }
	public int BookId { get; set; }
}
