using AutoMapper;
using BookShareHub.Application.Dto.Order;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.Application.MappingProfiles
{
    internal class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<OrderCreateDto, Order>();
			CreateMap<Order, DoneOrderTitleDto>();
			CreateMap<Order, ActualOrderTitleDto>();
			CreateMap<Order, OrderDto>();
		}
	}
}
