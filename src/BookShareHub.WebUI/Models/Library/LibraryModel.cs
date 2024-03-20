using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Filters;

namespace BookShareHub.WebUI.Models;

public class LibraryModel
{
    public required string UserId { get; set; }
    public required List<BookTitleDto> BookTitles { get; set; }
    public BooksLibraryFilter? FilterQuery { get; set; }
    public SearchFilter? SearchQuery { get; set; }
}
