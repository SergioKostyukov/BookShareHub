using BookShareHub.Application.Dto.Book;

namespace BookShareHub.WebUI.Models;

public class MyBooksLibraryModel
{
    public required List<BookTitleDto> BookTitles { get; init; }
}
