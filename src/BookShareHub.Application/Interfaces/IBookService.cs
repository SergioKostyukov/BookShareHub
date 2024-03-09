using BookShareHub.Core.Domain.Entities;
using BookShareHub.Application.Dto;
using Microsoft.AspNetCore.Http;

namespace BookShareHub.Application.Interfaces
{
	public interface IBookService
    {
		Task<IEnumerable<Book>> GetAllBooksAsync(string userId);
		Task<IEnumerable<Book>> GetBooksByUserId(string userId);
		Task<Book> GetBookByIdAsync(int id);
		Task AddBookAsync(BookDto book, IFormFile? imageFile);
		Task EditBookAsync(BookDto book, IFormFile? imageFile);
		Task DeleteBookAsync(int id);
	}
}
