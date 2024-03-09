using System.Net;
using BookShareHub.Application.DTOs;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShareHub.Application.Services
{
	internal class BookService(BookShareHubDbContext context) : IBookService
	{
		private readonly BookShareHubDbContext _context = context;

		// ----------------------- GET METHODS -----------------------
		// Search book by userId 
		public async Task<IEnumerable<Book>> GetBooksByUserId(string userId)
		{
			return await _context.Books
				.Where(b => b.OwnerId == userId)
				.ToListAsync();
		}

		// Search all books except those owned by the user
		public async Task<IEnumerable<Book>> GetAllBooksAsync(string userId)
		{
			return await _context.Books
				.Where(b => b.OwnerId != userId)
				.ToListAsync();
		}

		// Search book by bookId
		public async Task<Book> GetBookByIdAsync(int bookId)
		{
			return await _context.Books
				.FirstOrDefaultAsync(b => b.Id == bookId);
		}

		// ----------------------- PATCH METHODS -----------------------
		// Add book to DB
		public async Task AddBookAsync(BookDto bookDto)
		{
			var book = new Book
			{
				OwnerId = bookDto.OwnerId,
				Title = bookDto.Title,
				Author = bookDto.Author,
				Language = bookDto.Language,
				Description = bookDto.Description,
				Price = bookDto.Price
			};

			if (bookDto.ImageFile?.Length > 0)
			{
				string imageFolderPath = "wwwroot/images";
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

				string extension = Path.GetExtension(bookDto.ImageFile.FileName);
				string fileName = bookDto.OwnerId + DateTime.Now.ToString("yymmssfff") + extension;
				string path = Path.Combine(imageFolderPath, fileName);

				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					await bookDto.ImageFile.CopyToAsync(fileStream);
				}

				book.ImagePath = "/images/" + fileName;
			}

			_context.Books.Add(book);
			await _context.SaveChangesAsync();
		}

		// Edit book data in DB
		public async Task EditBookAsync(BookDto bookDto)
		{
			var book = new Book
			{
				Id = bookDto.Id,
				OwnerId = bookDto.OwnerId,
				Title = bookDto.Title,
				Author = bookDto.Author,
				Language = bookDto.Language,
				Description = bookDto.Description,
				Price = bookDto.Price
			};

			if (bookDto.ImageFile?.Length > 0)
			{
				await DeleteBookImage(bookDto.Id);

				string imageFolderPath = "wwwroot/images";
				string extension = Path.GetExtension(bookDto.ImageFile.FileName);
				string fileName = bookDto.OwnerId + DateTime.Now.ToString("yymmssfff") + extension;
				string path = Path.Combine(imageFolderPath, fileName);

				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					await bookDto.ImageFile.CopyToAsync(fileStream);
				}

				book.ImagePath = "/images/" + fileName;
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
	}
}
