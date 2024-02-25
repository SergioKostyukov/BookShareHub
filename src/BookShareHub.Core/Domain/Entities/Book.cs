namespace BookShareHub.Core.Domain.Entities;

public class Book
{
	public int Id { get; set; }
	public int OwnerId { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public string Language { get; set; }
	public string? Description { get; set; }
	public int Value { get; set; } // internal abstract currency
	public int Price { get; set; }
}
