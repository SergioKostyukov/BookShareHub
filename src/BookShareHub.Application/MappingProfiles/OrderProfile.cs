using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Application.MappingProfiles
{
	internal class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<OrderCreateDto, Order>();
		}
	}
}
