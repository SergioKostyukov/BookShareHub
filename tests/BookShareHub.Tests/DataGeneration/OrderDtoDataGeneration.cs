using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BookShareHub.Application.Dto.Book;
using BookShareHub.Application.Dto.Order;
using BookShareHub.Core.Domain.Enums;

namespace BookShareHub.Tests.DataGeneration
{
	public class OrderDtoDataGeneration
	{
		readonly Faker<OrderDto> orderDataFake;

		public OrderDtoDataGeneration()
		{
			orderDataFake = new Faker<OrderDto>()
			.CustomInstantiator(f => new OrderDto(
				f.Random.Int(1, 100),
				f.Random.Guid().ToString(),
				f.Random.Guid().ToString(),
				f.PickRandom<OrderStatus>(),
				f.PickRandom<OrderType>(),
				f.Date.Recent(3),
				f.Date.Recent(3),
				f.Random.String(),
				f.Random.Decimal(10, 200)
			));
		}

		public OrderDto GenerateOrder()
		{
			return orderDataFake.Generate();
		}

		public List<OrderDto> GenerateOrders(int count)
		{
			return orderDataFake.Generate(count);
		}
	}
}
