using Microsoft.AspNetCore.Http;

namespace BookShareHub.Application.DTOs;

public class BookDto
{
	public int Id { get; set; }
	public string? OwnerId { get; set; }
	public string Title { get; set; }
	public string Author { get; set; }
	public string Language { get; set; }
	public string? Description { get; set; }
	public decimal? Price { get; set; }
	public string? ImagePath { get; set; }
	public IFormFile? ImageFile { get; set; }
}

//public class EditBookImageDto
//{
//	public int Id { get; set; }
//	public IFormFile ImageFile { get; set; }
//}