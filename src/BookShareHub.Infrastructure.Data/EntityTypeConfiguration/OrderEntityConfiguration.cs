using Microsoft.EntityFrameworkCore;
using BookShareHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShareHub.Infrastructure.Data.EntityTypeConfiguration
{
	internal class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CustomerId)
				.IsRequired();

			builder.Property(x => x.OwnerId)
				.IsRequired();

			builder.Property(x => x.OrderStatus)
				.IsRequired()
				.HasConversion<int>();

			builder.Property(x => x.OrderType)
				.IsRequired()
				.HasConversion<int>();

			builder.Property(x => x.CreatedDate)
				.IsRequired();

			builder.Property(x => x.OrderDate)
				.IsRequired();

			builder.Property(x => x.CheckAmount)
				.IsRequired()
				.HasColumnType("decimal(8,2)");
		}
	}
}
