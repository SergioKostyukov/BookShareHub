using BookShareHub.Application.Dto.Book;

namespace BookShareHub.WebUI.Models;

public class MyBooksModel
{
    public required List<BookTitleDto> BookTitles { get; init; }
}
