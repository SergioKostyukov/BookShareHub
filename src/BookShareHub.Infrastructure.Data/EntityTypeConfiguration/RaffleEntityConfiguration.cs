using Microsoft.EntityFrameworkCore;
using BookShareHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShareHub.Infrastructure.Data.EntityTypeConfiguration
{
	internal class RaffleEntityConfiguration : IEntityTypeConfiguration<Raffle>
	{
		public void Configure(EntityTypeBuilder<Raffle> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.OwnerId)
				.IsRequired();

			builder.Property(x => x.Type)
				.IsRequired()
				.HasConversion<int>();

			builder.Property(x => x.TicketPrice)
				.IsRequired()
				.HasColumnType("decimal(8,2)");

			builder.Property(x => x.EndDateTime)
				.IsRequired();

			builder.Property(x => x.Description);

			builder.Property(x => x.IsActive)
				.IsRequired();
		}
	}
}
