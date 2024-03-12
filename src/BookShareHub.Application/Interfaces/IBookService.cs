using BookShareHub.Application.Dto;
using Microsoft.AspNetCore.Http;

namespace BookShareHub.Application.Interfaces
{
	public interface IBookService
    {
		Task AddBookAsync(BookDto book, IFormFile? imageFile);
		Task EditBookAsync(BookDto book, IFormFile? imageFile);
		Task DeleteBookAsync(int id);
	}
}
