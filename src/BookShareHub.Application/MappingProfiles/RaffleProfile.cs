using AutoMapper;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.Application.MappingProfiles
{
	internal class RaffleProfile : Profile
	{
		public RaffleProfile()
		{
			CreateMap<Raffle, RaffleTitleDto>();
			CreateMap<Raffle, RaffleDto>();
			CreateMap<RaffleDto, Raffle>();
			CreateMap<RaffleCreateDto, Raffle>();
		}
	}
}
