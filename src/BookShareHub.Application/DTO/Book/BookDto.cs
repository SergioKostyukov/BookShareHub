using System.ComponentModel.DataAnnotations;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.Dto.Book;

public class BookDto
{
    public int Id { get; set; }
    public string? OwnerId { get; set; } = string.Empty;
	[Required(ErrorMessage = "The Title field is required.")]
	public string Title { get; set; } = string.Empty;
	[Required(ErrorMessage = "The Author field is required.")]
	public string Author { get; set; } = string.Empty;
	[Required(ErrorMessage = "The Genre field is required.")]
	public BookGenre Genre { get; set; }
	[Required(ErrorMessage = "The Language field is required.")]
	public BookLanguage Language { get; set; }
    public string? Description { get; set; }
	[Required(ErrorMessage = "The Price field is required.")]
	[Range(0, double.MaxValue, ErrorMessage = "The Price field must be greater than or equal to 0.")]
	public decimal Price { get; set; }
	public string? ImagePath { get; set; } = string.Empty;
}
