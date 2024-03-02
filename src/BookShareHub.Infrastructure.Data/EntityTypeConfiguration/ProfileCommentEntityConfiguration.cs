using Microsoft.EntityFrameworkCore;
using BookShareHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShareHub.Infrastructure.Data.EntityTypeConfiguration
{
	internal class ProfileCommentEntityConfiguration : IEntityTypeConfiguration<ProfileComment>
	{
		public void Configure(EntityTypeBuilder<ProfileComment> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.AuthorId)
				.IsRequired();

			builder.Property(x => x.ProfileId)
				.IsRequired();

			builder.Property(x => x.Comment)
				.IsRequired();

			builder.Property(x => x.Mark)
				.IsRequired();

			builder.Property(x => x.Response);

			builder.Property(x => x.CreateDate)
				.IsRequired();
		}
	}
}
