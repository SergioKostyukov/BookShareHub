using Microsoft.EntityFrameworkCore;
using BookShareHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShareHub.Infrastructure.Data.EntityTypeConfiguration
{
	internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(x => x.Id)
				.IsRequired();

			builder.Property(x => x.Name)
				.IsRequired();

			builder.Property(x => x.Tag)
				.IsRequired();

			builder.Property(x => x.Email);

			builder.Property(x => x.Password)
				.IsRequired();

			builder.Property(x => x.Rating);
		}
	}
}
