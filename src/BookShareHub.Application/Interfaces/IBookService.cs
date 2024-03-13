using BookShareHub.Application.Dto;

namespace BookShareHub.Application.Interfaces
{
	public interface IBookService
    {
		Task AddBookAsync(BookDto book, ImageFileDto? imageFile);
		Task EditBookAsync(BookDto book, ImageFileDto? imageFile);
		Task DeleteBookAsync(int id);
	}
}
