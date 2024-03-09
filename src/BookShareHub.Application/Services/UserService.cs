using BookShareHub.Application.Interfaces;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.Application.Services
{
	internal class UserService : IUserService
	{
		public Task<User> GetUserById(string userId)
		{
			throw new NotImplementedException();
		}
	}
}
