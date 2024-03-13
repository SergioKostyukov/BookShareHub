using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Interfaces;
using BookShareHub.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class UserService(ILogger<UserService> logger, BookShareHubDbContext context, IMapper mapper) : IUserService
	{
		private readonly ILogger<UserService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		/* ----------------------- GET METHODS ----------------------- */
		public async Task<UserDto> GetUserByIdAsync(string userId)
		{
			var user = await _context.Users.FindAsync(userId);

			return _mapper.Map<UserDto>(user);
		}
	}
}
