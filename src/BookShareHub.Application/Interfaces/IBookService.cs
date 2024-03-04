using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShareHub.Core.Domain.Entities;


namespace BookShareHub.Application.Interfaces
{
    public interface IBookService
    {
		Task<IEnumerable<Book>> GetAllBooksAsync();
		Task<List<Book>> GetBooksByUserId(string userId);
		Task<Book> GetBookByIdAsync(int id);
		Task AddBookAsync(Book book);
		Task EditBookAsync(Book book);
		Task DeleteBookAsync(int id);
	}
}
