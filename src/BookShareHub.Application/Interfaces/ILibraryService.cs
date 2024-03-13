using BookShareHub.Application.Dto;
using BookShareHub.Application.Filters;

namespace BookShareHub.Application.Interfaces
{
	public interface ILibraryService
	{
		Task<List<BookTitleDto>> GetAllBooksAsync(string userId);
		Task<List<BookTitleDto>> GetAllBooksByUserIdAsync(string userId);
		Task<List<BookTitleDto>> GetAllBooksByFilterAsync(LibraryFilter filter, string userId);
		Task<List<BookTitleDto>> GetAllBooksBySearchAsync(LibrarySearch request, string userId);
		Task<List<BookTitleDto>> GetAllBooksByOrderIdAsync(int orderId);
		Task<BookDto> GetBookByIdAsync(int id);
	}
}
