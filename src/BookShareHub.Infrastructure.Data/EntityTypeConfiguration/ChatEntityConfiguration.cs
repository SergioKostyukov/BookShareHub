using Microsoft.EntityFrameworkCore;
using BookShareHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShareHub.Infrastructure.Data.EntityTypeConfiguration
{
	internal class ChatEntityConfiguration : IEntityTypeConfiguration<Chat>
	{
		public void Configure(EntityTypeBuilder<Chat> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.AdminId)
				.IsRequired();

			builder.Property(x => x.Title)
				.IsRequired();

			builder.Property(x => x.Description);

			builder.Property(x => x.CreateDate)
				.IsRequired();

			builder.Property(x => x.ChatType)
				.IsRequired()
				.HasConversion<int>();

			builder.Property(x => x.Messages)
				.IsRequired();
		}
	}
}
