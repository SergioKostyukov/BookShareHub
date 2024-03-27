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
			CreateMap<OrderTemplateCreateDto, Order>();
			CreateMap<Order, DoneOrderTitleDto>();
			CreateMap<Order, ActualOrderTitleDto>();
			CreateMap<Order, ActualTemplatedOrderDto>();
			CreateMap<Order, OrderDto>();
			CreateMap<Order, ConfirmedOrderDto>();
		}
	}
}
