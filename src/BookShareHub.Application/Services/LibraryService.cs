﻿using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Filters;
using BookShareHub.Application.Interfaces;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class LibraryService(ILogger<LibraryService> logger, BookShareHubDbContext context, IMapper mapper) : ILibraryService
	{
		private readonly ILogger<LibraryService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		/* ----------------------- GET METHODS ----------------------- */
		// Get all books except those owned by the user
		public async Task<List<BookTitleDto>> GetAllBooksAsync(string userId)
		{
			var books = await _context.Books
				.Where(b => b.OwnerId != userId)
				.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		// Get all books owned by this user
		public async Task<List<BookTitleDto>> GetAllBooksByUserIdAsync(string userId)
		{
			var books = await _context.Books
				.Where(b => b.OwnerId == userId)
				.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		// Get all books by filter except those owned by the user
		public async Task<List<BookTitleDto>> GetAllBooksByFilterAsync(LibraryFilter filter, string userId)
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

			// Exclude books owned by the user
			query = query
				.Where(b => b.OwnerId != userId);

			var books = await query.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		// Get all books by search parameter except those owned by the user
		public async Task<List<BookTitleDto>> GetAllBooksBySearchAsync(LibrarySearch request, string userId)
		{
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

		// Get book details by bookId
		public async Task<BookDto> GetBookByIdAsync(int bookId)
		{
			var book = await _context.Books
				.FirstOrDefaultAsync(b => b.Id == bookId);

			return _mapper.Map<BookDto>(book);
		}

		// Get book details by bookId
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
