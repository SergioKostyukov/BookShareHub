using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShareHub.Application.Services
{
	internal class BookService(BookShareHubDbContext context) : IBookService
    {
        private readonly BookShareHubDbContext _context = context;

		// Search book by userId 
        public async Task<List<Book>> GetBooksByUserId(string userId)
        {
            return await _context.Books.Where(b => b.OwnerId == userId).ToListAsync();
        }

		// Search all books except those owned by the user
		public async Task<IEnumerable<Book>> GetAllBooksAsync(string userId)
		{
			return await _context.Books.Where(b => b.OwnerId != userId).ToListAsync();
		}

		// Search book by bookId
		public async Task<Book> GetBookByIdAsync(int bookId)
		{
			return await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
		}

		// Add book to DB
		public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

		// Edit book data in DB
		public async Task EditBookAsync(Book book)
		{
			_context.Books.Update(book);
			await _context.SaveChangesAsync();
		}

		// Delete book from DB
		public async Task DeleteBookAsync(int id)
		{
			var book = await _context.Books.FindAsync(id) ?? throw new InvalidOperationException("Book not found");

			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
		}
	}
}
