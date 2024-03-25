using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Application.Filters;

namespace BookShareHub.Application.Interfaces
{
	public interface IRafflesLibraryService
	{
		Task<RaffleDto> GetRaffleById(int raffleId);
		Task<List<RaffleTitleDto>> GetActualRafflesAsync(string userId);
		Task<List<RaffleTitleDto>> GetAllRafflesAsync(string userId);
		Task<List<RaffleTitleDto>> GetAllRafflesByUserIdAsync(string userId);
		Task<List<RaffleTitleDto>> GetAllRafflesByFilterAsync(RafflesLibraryFilter filter, string userId);
		Task<RaffleDto> GetRaffleByIdAsync(int id);
	}
}
