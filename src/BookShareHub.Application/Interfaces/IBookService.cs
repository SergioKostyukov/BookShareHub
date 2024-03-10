using BookShareHub.Core.Domain.Entities;
using BookShareHub.Application.Dto;
using Microsoft.AspNetCore.Http;

namespace BookShareHub.Application.Interfaces
{
	public interface IBookService
    {
		Task<List<BookTitleDto>> GetAllBooksAsync(string userId);
		Task<List<BookTitleDto>> GetBooksByUserId(string userId);
		Task<BookDto> GetBookByIdAsync(int id);
		Task AddBookAsync(BookDto book, IFormFile? imageFile);
		Task EditBookAsync(BookDto book, IFormFile? imageFile);
		Task DeleteBookAsync(int id);
	}
}
