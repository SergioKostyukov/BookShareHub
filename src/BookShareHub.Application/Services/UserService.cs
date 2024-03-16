using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Application.Interfaces;
using BookShareHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookShareHub.Application.Services
{
	internal class UserService(ILogger<UserService> logger, BookShareHubDbContext context, IMapper mapper) : IUserService
	{
		private readonly ILogger<UserService> _logger = logger;
		private readonly BookShareHubDbContext _context = context;
		private readonly IMapper _mapper = mapper;

		public async Task<UserDto> GetUserByIdAsync(string userId)
		{
			var user = await _context.AspNetUsers.FindAsync(userId);

			return _mapper.Map<UserDto>(user);
		}

		public async Task<string> GetUserNameByIdAsync(string userId)
		{
			var userName = await _context.AspNetUsers
								 .Where(x => x.Id == userId)
								 .Select(x => x.UserName)
								 .FirstOrDefaultAsync() ?? throw new InvalidOperationException("User not found");

			return userName;
		}
	}
}
