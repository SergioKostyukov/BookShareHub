using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models;

public class BookModel
{
	public BookDto Book { get; set; } = new BookDto();

	public IFormFile? ImageFile { get; set; }
}
