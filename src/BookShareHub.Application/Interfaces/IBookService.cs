using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;

namespace BookShareHub.Application.Interfaces
{
    public interface IBookService
    {
		Task AddBookAsync(BookDto book, ImageFileDto imageFile);
		Task EditBookAsync(BookDto book, ImageFileDto? imageFile);
		Task DeleteBookAsync(int id);
	}
}
