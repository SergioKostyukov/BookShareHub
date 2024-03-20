using AutoMapper;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Filters;
using BookShareHub.Application.Interfaces;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class BooksLibraryService(ILogger<BooksLibraryService> logger, 
								  BookShareHubDbContext context, 
								  IMapper mapper) : IBooksLibraryService
	{
		private readonly ILogger<BooksLibraryService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		public async Task<List<BookTitleDto>> GetAllBooksAsync(string userId)
		{
			var books = await _context.Books
				.Where(b => b.OwnerId != userId && b.IsActive == true)
				.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		public async Task<List<BookTitleDto>> GetAllBooksByUserIdAsync(string userId)
		{
			var books = await _context.Books
				.Where(b => b.OwnerId == userId && b.IsActive == true)
				.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		public async Task<List<BookTitleDto>> GetAllBooksByFilterAsync(BooksLibraryFilter filter, string userId)
		{
			var query = _context.Books.AsQueryable();

			if (filter.SelectedLanguage.HasValue)
			{
				query = query
					.Where(b => b.Language == filter.SelectedLanguage.Value);
			}
			if (filter.SelectedGenre.HasValue)
			{
				query = query
					.Where(b => b.Genre == filter.SelectedGenre.Value);
			}
			if (filter.MaxPrice.HasValue)
			{
				query = query
					.Where(b => b.Price <= filter.MaxPrice.Value);
			}

			query = query
				.Where(b => b.OwnerId != userId && b.IsActive == true);

			var books = await query.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		public async Task<List<BookTitleDto>> GetAllBooksBySearchAsync(SearchFilter request, string userId)
		{
			var query = _context.Books.AsQueryable();

			if (!string.IsNullOrEmpty(request.Request))
			{
				string searchTerm = request.Request.ToLower();

				query = query
					.Where(b => b.Author.ToLower().Contains(searchTerm) ||
								b.Title.ToLower().Contains(searchTerm));
			}

			query = query
				.Where(b => b.OwnerId != userId)
				.Where(b => b.IsActive == true);

			var books = await query.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		public async Task<BookDto> GetBookByIdAsync(int bookId)
		{
			var book = await _context.Books
				.Where(b => b.Id == bookId)
				.FirstOrDefaultAsync();

			return _mapper.Map<BookDto>(book);
		}

		public async Task<List<BookTitleDto>> GetAllBooksByOrderIdAsync(int orderId)
		{
			var orderList = await _context.OrdersLists
				.Where(b => b.OrderId == orderId)
				.Select(x => x.BookId)
				.ToListAsync();

			var books = await _context.Books
				.Where(book => orderList.Contains(book.Id))
				.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}
	}
}
