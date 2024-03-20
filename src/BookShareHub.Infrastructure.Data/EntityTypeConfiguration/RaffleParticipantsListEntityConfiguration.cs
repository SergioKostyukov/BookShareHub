using BookShareHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookShareHub.Infrastructure.Data.EntityTypeConfiguration
{
	internal class RaffleParticipantsListEntityConfiguration : IEntityTypeConfiguration<RaffleParticipantsList>
	{
		public void Configure(EntityTypeBuilder<RaffleParticipantsList> builder)
		{
			builder.HasKey(x => new { x.RaffleId, x.RaffleUserId });

			builder.Property(x => x.TicketsCount)
				.IsRequired();

			builder.Property(x => x.ParticipationTime)
				.IsRequired();
		}
	}
}
