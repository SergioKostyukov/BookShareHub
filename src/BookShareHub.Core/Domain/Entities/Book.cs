using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Core.Domain.Entities;

public class Book
{
	public int Id { get; set; }
	public string OwnerId { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string Author { get; set; } = string.Empty;
	public BookGenre Genre { get; set; } 
	public BookLanguage Language { get; set; }
	public string? Description { get; set; }
	public decimal? Price { get; set; }
	public string? ImagePath { get; set; }
}
