using AutoMapper;
using BookShareHub.Application.Dto;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.Application.MappingProfiles
{
	internal class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<OrderCreateDto, Order>();
			CreateMap<Order, DoneOrderDetailsDto>();
			CreateMap<Order, OrderDto>();
		}
	}
}
