using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.Application.MappingProfiles
{
	internal class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDto>();
		}
	}
}
