using Microsoft.EntityFrameworkCore;
using BookShareHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShareHub.Infrastructure.Data.EntityTypeConfiguration
{
	internal class ChatSubscribersListEntityConfiguration : IEntityTypeConfiguration<ChatSubscribersList>
	{
		public void Configure(EntityTypeBuilder<ChatSubscribersList> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.ChatId)
				.IsRequired();

			builder.Property(x => x.UserId)
				.IsRequired();

			builder.Property(x => x.Status);

			builder.Property(x => x.AddedTime)
				.IsRequired();
		}
	}
}
