﻿using Microsoft.EntityFrameworkCore;
using BookShareHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShareHub.Infrastructure.Data.EntityTypeConfiguration
{
	internal class OrderListEntityConfiguration : IEntityTypeConfiguration<OrderList>
	{
		public void Configure(EntityTypeBuilder<OrderList> builder)
		{
			builder.Property(x => x.Id)
				.IsRequired();

			builder.Property(x => x.OrderId)
				.IsRequired();

			builder.Property(x => x.UserID)
				.IsRequired();

			builder.Property(x => x.BookId)
				.IsRequired();
		}
	}
}