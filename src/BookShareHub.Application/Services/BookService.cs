using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BookShareHub.Application.Services
{
	internal class BookService(BookShareHubDbContext context, IMapper mapper) : IBookService
	{
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		// ----------------------- GET METHODS -----------------------
		// Search book by userId 
		public async Task<List<BookTitleDto>> GetBooksByUserId(string userId)
		{
			var books = await _context.Books
				.Where(b => b.OwnerId == userId)
				.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		// Search all books except those owned by the user
		public async Task<List<BookTitleDto>> GetAllBooksAsync(string userId)
		{
			var books = await _context.Books
				.Where(b => b.OwnerId != userId)
				.ToListAsync();

			return _mapper.Map<List<BookTitleDto>>(books);
		}

		// Search book by bookId
		public async Task<BookDto> GetBookByIdAsync(int bookId)
		{
			var book = await _context.Books
				.FirstOrDefaultAsync(b => b.Id == bookId);

			return _mapper.Map<BookDto>(book);
		}

		// ----------------------- PATCH METHODS -----------------------
		// Add book to DB and save image to storage
		public async Task AddBookAsync(BookDto bookDto, IFormFile? imageFile)
		{
			var book = _mapper.Map<Book>(bookDto);

			if (imageFile?.Length > 0)
			{
				string imageFolderPath = "wwwroot/images";
				CreateImagesDirectory(imageFolderPath);

				await CopyImageAsync(book, imageFile, imageFolderPath);
			}

			_context.Books.Add(book);
			await _context.SaveChangesAsync();
		}

		// Edit book data in DB and image file in storage
		public async Task EditBookAsync(BookDto bookDto, IFormFile? imageFile)
		{
			var book = _mapper.Map<Book>(bookDto);

			if (imageFile?.Length > 0)
			{
				await DeleteBookImage(book.Id);

				string imageFolderPath = "wwwroot/images";
				await CopyImageAsync(book, imageFile, imageFolderPath);
			}
			else
			{
				book.ImagePath = bookDto.ImagePath;
			}

			_context.Books.Update(book);
			await _context.SaveChangesAsync();
		}

		// Delete book from DB
		public async Task DeleteBookAsync(int id)
		{
			var book = await _context.Books.FindAsync(id) ?? throw new InvalidOperationException("Book not found");

			await DeleteBookImage(id);

			_context.Books.Remove(book);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Copy image to server storage folder
		/// </summary>
		/// <param name="book">The book object whose image is stored.</param>
		/// <param name="imageFile">The actual image file object.</param>
		/// <param name="imageFolderPath">The path where the image should be saved on the server.</param>
		/// <returns></returns>
		private static async Task CopyImageAsync(Book book, IFormFile? imageFile, string imageFolderPath)
		{
			string extension = Path.GetExtension(imageFile.FileName);
			string fileName = book.OwnerId + DateTime.Now.ToString("yymmssfff") + extension;
			string path = Path.Combine(imageFolderPath, fileName);

			using (var fileStream = new FileStream(path, FileMode.Create))
			{
				await imageFile.CopyToAsync(fileStream);
			}

			book.ImagePath = "/images/" + fileName;
		}

		// Delete image file by bookId
		private async Task DeleteBookImage(int bookId)
		{
			var oldImagePath = await _context.Books
				.Where(b => b.Id == bookId)
				.Select(b => b.ImagePath)
				.FirstOrDefaultAsync();

			if (!string.IsNullOrEmpty(oldImagePath))
			{
				string oldImagePathPhysical = Path.Combine("wwwroot", oldImagePath.TrimStart('/'));
				if (File.Exists(oldImagePathPhysical))
				{
					File.Delete(oldImagePathPhysical);
				}
			}
		}

		// Create a new image directory (if it doesn't exist)
		private static void CreateImagesDirectory(string imageFolderPath)
		{
			if (!Directory.Exists(imageFolderPath))
			{
				try
				{
					Directory.CreateDirectory(imageFolderPath);
					Console.WriteLine("Directory created successfully.");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error creating directory: {ex.Message}");
				}
			}
		}
	}
}
