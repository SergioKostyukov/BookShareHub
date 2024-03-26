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

			builder.Property(x => x.Status)
				.IsRequired()
				.HasConversion<int>();

			builder.Property(x => x.Type)
				.IsRequired()
				.HasConversion<int>();

			builder.Property(x => x.CreateDate)
				.IsRequired();

			builder.Property(x => x.CloseDate);

			builder.Property(x => x.CheckAmount)
				.IsRequired()
				.HasColumnType("decimal(8,2)");

			builder.Property(x => x.DeliveryAddress);
			builder.Property(x => x.DeliveryUser);
			builder.Property(x => x.DeliveryUserPhone);
		}
	}
}
