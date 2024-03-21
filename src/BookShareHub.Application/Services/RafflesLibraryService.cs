using AutoMapper;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Application.Filters;
using BookShareHub.Application.Interfaces;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BookShareHub.Application.Services
{
	internal class RafflesLibraryService(ILogger<RafflesLibraryService> logger,
								  BookShareHubDbContext context,
								  IMapper mapper) : IRafflesLibraryService
	{
		private readonly ILogger<RafflesLibraryService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;
		private const string DefaultImagePath = "https://storage.googleapis.com/book_share_hub_books_images/photo.jpg";

		public async Task<List<RaffleTitleDto>> GetAllRafflesAsync(string userId)
		{
			var raffles = await _context.Raffles
				.Where(b => b.OwnerId != userId && b.IsActive == true)
				.ToListAsync();

			var rafflesTitles = _mapper.Map<List<RaffleTitleDto>>(raffles);

			foreach(var raf in rafflesTitles)
			{
				var bookId = await _context.OrdersLists
										.Where(x => x.OrderId == raf.OrderId)
										.Select(x => x.BookId)
										.FirstOrDefaultAsync();

				raf.ImagePath = await _context.Books
										.Where(b => b.Id == bookId)
										.Select(b => b.ImagePath)
										.FirstOrDefaultAsync() ?? DefaultImagePath;
 			}

			return rafflesTitles;
		}

		public async Task<List<RaffleTitleDto>> GetAllRafflesByUserIdAsync(string userId)
		{
			var raffles = await _context.Raffles
				.Where(b => b.OwnerId == userId && b.IsActive == true)
				.ToListAsync();

			return _mapper.Map<List<RaffleTitleDto>>(raffles);
		}

		public async Task<List<RaffleTitleDto>> GetAllRafflesByFilterAsync(RafflesLibraryFilter filter, string userId)
		{
			var query = _context.Raffles.AsQueryable();

			if (filter.SelectedType.HasValue)
			{
				query = query
					.Where(b => b.Type == filter.SelectedType.Value);
			}
			if (filter.SelectedMaxTicketPrice.HasValue)
			{
				query = query
					.Where(b => b.TicketPrice <= filter.SelectedMaxTicketPrice.Value);
			}

			query = query
				.Where(b => b.OwnerId != userId && b.IsActive == true);

			var books = await query.ToListAsync();

			return _mapper.Map<List<RaffleTitleDto>>(books);
		}

		public async Task<RaffleDto> GetRaffleByIdAsync(int raffleId)
		{
			var raffle = await _context.Raffles
				.Where(b => b.Id == raffleId)
				.FirstOrDefaultAsync();

			return _mapper.Map<RaffleDto>(raffle);
		}
	}
}
