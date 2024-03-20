using AutoMapper;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Application.Filters;
using BookShareHub.Application.Interfaces;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class RafflesLibraryService(ILogger<RafflesLibraryService> logger,
								  BookShareHubDbContext context,
								  IMapper mapper) : IRafflesLibraryService
	{
		private readonly ILogger<RafflesLibraryService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		public async Task<List<RaffleTitleDto>> GetAllRafflesAsync(string userId)
		{
			var raffles = await _context.Raffles
				.Where(b => b.OwnerId != userId && b.IsActive == true)
				.ToListAsync();

			return _mapper.Map<List<RaffleTitleDto>>(raffles);
		}

		public async Task<List<RaffleTitleDto>> GetAllRafflesByFilterAsync(RafflesLibraryFilter filter, string userId)
		{
			var query = _context.Raffles.AsQueryable();

			if (filter.SelectedLanguage.HasValue)
			{
				query = query
					.Where(b => b.Language == filter.SelectedLanguage.Value);
			}
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

		public async Task<List<RaffleTitleDto>> GetAllRafflesBySearchAsync(SearchFilter request, string userId)
		{
			var query = _context.Raffles.AsQueryable();

			if (!string.IsNullOrEmpty(request.Request))
			{
				string searchTerm = request.Request.ToLower();

				query = query
					.Where(b => b.Title.ToLower().Contains(searchTerm));
			}

			query = query
				.Where(b => b.OwnerId != userId)
				.Where(b => b.IsActive == true);

			var books = await query.ToListAsync();

			return _mapper.Map<List<RaffleTitleDto>>(books);
		}

		public Task<List<RaffleTitleDto>> GetAllRafflesByUserIdAsync(string userId)
		{
			throw new NotImplementedException();
		}

		public Task<RaffleDto> GetRaffleByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
