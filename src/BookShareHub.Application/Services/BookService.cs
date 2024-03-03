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

        public async Task AddBook(Book book)
        {
            Console.WriteLine(book.Title, book.Author);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
    }
}
