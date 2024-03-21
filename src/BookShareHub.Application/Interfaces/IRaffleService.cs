using BookShareHub.Application.Dto.Raffle;

namespace BookShareHub.Application.Interfaces
{
	public interface IRaffleService
    {
        Task AddRaffleAsync(RaffleCreateDto request);
		Task<List<RaffleTitleDto>> GetActualRafflesAsync(string userId);
	}
}
