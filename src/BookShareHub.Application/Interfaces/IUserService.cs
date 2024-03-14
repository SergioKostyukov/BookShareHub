using BookShareHub.Core.Domain.Entities;
using BookShareHub.Application.Dto;

namespace BookShareHub.Application.Interfaces
{
	public interface IUserService
	{
		Task<UserDto> GetUserByIdAsync(string userId);
		Task<string> GetUserNameByIdAsync(string userId);
	}
}
