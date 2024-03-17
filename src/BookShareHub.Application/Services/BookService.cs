using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using BookShareHub.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class BookService(ILogger<BookService> logger,
							   BookShareHubDbContext context,
							   IMapper mapper,
							   IImageKeeperService imageKeeperService) : IBookService
	{
		private readonly ILogger<BookService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;
		private readonly IImageKeeperService _imageKeeperService = imageKeeperService;

		public async Task AddBookAsync(BookDto bookDto, ImageFileDto? imageFile)
		{
			if (imageFile?.ImageFile == null || imageFile.ImageFile.Length == 0)
			{
				throw new ArgumentException("Error! Empty image file!");
			}

			using (var stream = imageFile.ImageFile.OpenReadStream())
			{
				bookDto.ImagePath = await _imageKeeperService.UploadImageAsync(stream, imageFile.ImageFile.FileName);
				_logger.LogInformation("Image saved with URL: " + bookDto.ImagePath);
			}

			_context.Books.Add(_mapper.Map<Book>(bookDto));
			await _context.SaveChangesAsync();
		}

		public async Task EditBookAsync(BookDto bookDto, ImageFileDto? imageFile)
		{
			if (imageFile?.ImageFile?.Length > 0)
			{
				using (var stream = imageFile.ImageFile.OpenReadStream())
				{
					await _imageKeeperService.DeleteImageAsync(bookDto.ImagePath);
					bookDto.ImagePath = await _imageKeeperService.UploadImageAsync(stream, imageFile.ImageFile.FileName);
				}
			}

			_context.Books.Update(_mapper.Map<Book>(bookDto));
			await _context.SaveChangesAsync();
		}

		public async Task DeleteBookAsync(int bookId)
		{
			var book = await _context.Books.FindAsync(bookId) ?? throw new InvalidOperationException("Book not found");

			await _imageKeeperService.DeleteImageAsync(book.ImagePath);
			_logger.LogInformation("Image deleted");


			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
		}
	}
}
