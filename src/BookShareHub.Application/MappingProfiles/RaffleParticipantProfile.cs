using AutoMapper;
using BookShareHub.Application.Dto.Raffle;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.Application.MappingProfiles
{
	internal class RaffleParticipantProfile : Profile
	{
		public RaffleParticipantProfile()
		{
			CreateMap<AddRaffleParticipantDto, RaffleParticipantsList>();
		}
	}
}
