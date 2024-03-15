using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Book;

public class BookDto
{
    public int Id { get; set; }
    public string OwnerId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public BookGenre Genre { get; set; }
    public BookLanguage Language { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; } = string.Empty;
}
