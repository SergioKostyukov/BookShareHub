using BookShareHub.Application.Dto.Book;

namespace BookShareHub.WebUI.Models;

public class EditBookModel
{
    public required BookDto Book { get; init; }

    public IFormFile? ImageFile { get; set; }
}
