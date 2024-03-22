using AutoMapper;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class RaffleService(ILogger<OrderService> logger,
								BookShareHubDbContext context,
								IMapper mapper,
								IOrderService orderService) : IRaffleService
	{
		private readonly ILogger<OrderService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;
		private readonly IOrderService _orderService = orderService;

		public async Task AddRaffleAsync(RaffleCreateDto request)
		{
			var raffle = _mapper.Map<Raffle>(request);
			_context.Raffles.Add(raffle);

			await _orderService.ConfirmOrderTemplateAsync(request.OrderId);

			await _context.SaveChangesAsync();
		}

		public async Task<List<RaffleTitleDto>> GetActualRafflesAsync(string userId)
		{
			var raffles = await _context.Raffles
				.Where(b => (b.OwnerId == userId && b.IsActive))
				.ToListAsync();

			var raffleTitleList = _mapper.Map<List<RaffleTitleDto>>(raffles);

			return raffleTitleList;
		}
	}
}
