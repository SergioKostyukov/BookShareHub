using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
    internal class BookService(ILogger<BookService> logger, BookShareHubDbContext context, IMapper mapper) : IBookService
	{
		private readonly ILogger<BookService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		public async Task AddBookAsync(BookDto bookDto, ImageFileDto? imageFile)
		{
			// Add image to storage
			if (imageFile?.ImageFile?.Length > 0)
			{
				string imageFolderPath = "wwwroot/images";
				CreateImagesDirectory(imageFolderPath);

				await CopyImageAsync(bookDto, imageFile, imageFolderPath);
			}

			_context.Books.Add(_mapper.Map<Book>(bookDto));
			await _context.SaveChangesAsync();
		}

		public async Task EditBookAsync(BookDto bookDto, ImageFileDto? imageFile)
		{
			// Update image in storage
			if (imageFile?.ImageFile?.Length > 0)
			{
				DeleteBookImage(bookDto.ImagePath);

				string imageFolderPath = "wwwroot/images";
				await CopyImageAsync(bookDto, imageFile, imageFolderPath);
			}

			_context.Books.Update(_mapper.Map<Book>(bookDto));
			await _context.SaveChangesAsync();
		}

		public async Task DeleteBookAsync(int bookId)
		{
			var book = await _context.Books.FindAsync(bookId) ?? throw new InvalidOperationException("Book not found");

			DeleteBookImage(book.ImagePath);

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
		private static async Task CopyImageAsync(BookDto book, ImageFileDto? imageFile, string imageFolderPath)
		{
			string extension = Path.GetExtension(imageFile.ImageFile.FileName);
			string fileName = book.OwnerId + DateTime.Now.ToString("yymmssfff") + extension;
			string path = Path.Combine(imageFolderPath, fileName);

			using (var fileStream = new FileStream(path, FileMode.Create))
			{
				await imageFile.ImageFile.CopyToAsync(fileStream);
			}

			book.ImagePath = "/images/" + fileName;
		}

		private static void DeleteBookImage(string oldImagePath)
		{
			if (!string.IsNullOrEmpty(oldImagePath))
			{
				string oldImagePathPhysical = Path.Combine("wwwroot", oldImagePath.TrimStart('/'));
				if (File.Exists(oldImagePathPhysical))
				{
					File.Delete(oldImagePathPhysical);
				}
			}
		}

		private void CreateImagesDirectory(string imageFolderPath)
		{
			if (!Directory.Exists(imageFolderPath))
			{
				try
				{
					Directory.CreateDirectory(imageFolderPath);
					_logger.LogInformation("Directory created successfully.");
				}
				catch (Exception ex)
				{
					_logger.LogError($"Error creating directory: {ex.Message}");
				}
			}
		}
	}
}
