using BookShareHub.Core.Domain.Entities;
using BookShareHub.Application.DTOs;

namespace BookShareHub.Application.Interfaces
{
	public interface IBookService
    {
		Task<IEnumerable<Book>> GetAllBooksAsync(string userId);
		Task<List<Book>> GetBooksByUserId(string userId);
		Task<Book> GetBookByIdAsync(int id);
		Task AddBookAsync(BookDto book);
		Task EditBookAsync(BookDto book);
		Task DeleteBookAsync(int id);
	}
}

// Task EditBookImageAsync(EditBookImageDto book);
// Task UploadBookImageAsync(EditBookImageDto book);