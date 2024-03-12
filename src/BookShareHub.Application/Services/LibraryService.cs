using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Filters;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class LibraryService(ILogger<BookService> logger, BookShareHubDbContext context, IMapper mapper) : ILibraryService
	{
		private readonly BookShareHubDbContext _context = context;
		private readonly ILogger<BookService> _logger = logger;
		private readonly IMapper _mapper = mapper;

		// ----------------------- GET METHODS -----------------------

		// Search all books except those owned by the user
		public async Task<List<BookTitleDto>> GetAllBooksAsync(string userId)
		{
			var books = await _context.Books
				.Where(b => b.OwnerId != userId)
				.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		// Search book by userId 
		public async Task<List<BookTitleDto>> GetAllBooksByUserIdAsync(string userId)
		{
			var books = await _context.Books
				.Where(b => b.OwnerId == userId)
				.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		// 
		async public Task<List<BookTitleDto>> GetAllBooksByFilterAsync(LibraryFilter filter, string userId)
		{
			//_logger.LogInformation("{0} {1}", filter.SelectedLanguage, filter.SelectedGenre);
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

			// Exclude books owned by the user
			query = query
				.Where(b => b.OwnerId != userId);

			var books = await query.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		async public Task<List<BookTitleDto>> GetAllBooksBySearchAsync(LibrarySearch request, string userId)
		{
			//_logger.LogInformation("{0}", request.Request);
			var query = _context.Books.AsQueryable();

			if (!string.IsNullOrEmpty(request.Request))
			{
				string searchTerm = request.Request.ToLower();

				// Filter books where either Author or Name contains the search term
				query = query
					.Where(b => b.Author.ToLower().Contains(searchTerm) ||
								b.Title.ToLower().Contains(searchTerm));
			}

			// Exclude books owned by the user
			query = query
				.Where(b => b.OwnerId != userId);

			var books = await query.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		// Find book by bookId
		public async Task<BookDto> GetBookByIdAsync(int bookId)
		{
			var book = await _context.Books
				.FirstOrDefaultAsync(b => b.Id == bookId);

			return _mapper.Map<BookDto>(book);
		}
	}
}
