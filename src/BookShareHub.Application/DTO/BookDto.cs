using Microsoft.AspNetCore.Http;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.DTOs;

public class BookDto
{
	public int Id { get; set; }
	public string? OwnerId { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public BookLanguage Language { get; set; }
	public string? Description { get; set; }
	public decimal? Price { get; set; }
	public string? ImagePath { get; set; }
	public IFormFile? ImageFile { get; set; }
}
