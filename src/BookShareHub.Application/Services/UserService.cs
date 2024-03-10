using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Interfaces;
using BookShareHub.Infrastructure.Data;

namespace BookShareHub.Application.Services
{
	internal class UserService(BookShareHubDbContext context, IMapper mapper) : IUserService
	{
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		public async Task<UserDto> GetUserByIdAsync(string userId)
		{
			var user = await _context.Users.FindAsync(userId);

			return _mapper.Map<UserDto>(user);
		}
	}
}
