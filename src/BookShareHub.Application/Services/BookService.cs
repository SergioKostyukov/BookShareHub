using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShareHub.Application.Services
{
    internal class BookService(BookShareHubDbContext context) : IBookService
    {
        private readonly BookShareHubDbContext _context = context;

        public async Task<List<Book>> GetBooksByUserId(string userId)
        {
            return await _context.Books.Where(b => b.OwnerId == userId).ToListAsync();
        }

        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

		public Task<IEnumerable<Book>> GetAllBooksAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<Book> GetBookByIdAsync(int id)
		{
			return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
		}

		public async Task EditBookAsync(Book book)
		{
			_context.Books.Update(book);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteBookAsync(int id)
		{
			var book = await _context.Books.FindAsync(id) ?? throw new InvalidOperationException("Book not found");

			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
		}
	}
}
