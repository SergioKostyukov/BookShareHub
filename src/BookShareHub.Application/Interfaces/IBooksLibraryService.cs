using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Filters;

namespace BookShareHub.Application.Interfaces
{
    public interface IBooksLibraryService
	{
		Task<List<BookTitleDto>> GetAllBooksAsync(string userId);
		Task<List<BookTitleDto>> GetAllBooksByUserIdAsync(string userId);
		Task<List<BookTitleDto>> GetAllBooksByFilterAsync(BooksLibraryFilter filter, string userId);
		Task<List<BookTitleDto>> GetAllBooksBySearchAsync(SearchFilter request, string userId);
		Task<List<BookTitleDto>> GetAllBooksByOrderIdAsync(int orderId);
		Task<BookDto> GetBookByIdAsync(int id);
	}
}
