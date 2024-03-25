using AutoMapper;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Application.Filters;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
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

		public async Task<RaffleDto> GetRaffleById(int raffleId)
		{
			var raffle = await _context.Raffles
				.Where(r => r.Id == raffleId)
				.FirstOrDefaultAsync();

			return _mapper.Map<RaffleDto>(raffle);
		}

		public async Task<List<RaffleTitleDto>> GetActualRafflesAsync(string userId)
		{
			var raffles = await _context.Raffles
				.Where(b => (b.OwnerId == userId && b.IsActive))
				.ToListAsync();

			var raffleTitleList = _mapper.Map<List<RaffleTitleDto>>(raffles);

			return raffleTitleList;
		}
		public async Task<List<RaffleTitleDto>> GetAllRafflesAsync(string userId)
		{
			var raffles = await _context.Raffles
				.Where(b => b.OwnerId != userId && b.IsActive == true)
				.ToListAsync();

			var rafflesTitles = _mapper.Map<List<RaffleTitleDto>>(raffles);

			foreach (var raf in rafflesTitles)
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

			// There are many books in orderLists for each raffle, and after join they create a new raffle line for each book
			//return await(from raffle in _context.Raffles
			//						where raffle.OwnerId != userId && raffle.IsActive
			//						join orderList in _context.OrdersLists on raffle.OrderId equals orderList.OrderId
			//						join book in _context.Books on orderList.BookId equals book.Id
			//						select new RaffleTitleDto
			//							   {
			//								   Id = raffle.Id,
			//								   OrderId = orderList.OrderId,
			//								   Type = raffle.Type,
			//								   TicketPrice = raffle.TicketPrice,
			//								   EndDateTime = raffle.EndDateTime,
			//								   ImagePath = book != null ? book.ImagePath : DefaultImagePath
			//							   }).ToListAsync();
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
