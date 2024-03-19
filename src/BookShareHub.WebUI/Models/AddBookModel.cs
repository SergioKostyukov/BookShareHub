using System.ComponentModel.DataAnnotations;
using BookShareHub.Application.Dto.Book;

namespace BookShareHub.WebUI.Models;

public class AddBookModel
{
	[Required(ErrorMessage = "The Book field is required.")]
	public required BookDto Book { get; init; }

	[Required(ErrorMessage = "An image file is required.")]
	public required IFormFile ImageFile { get; set; }
}
