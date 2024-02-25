﻿using Microsoft.EntityFrameworkCore;
using BookShareHub.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShareHub.Infrastructure.Data.EntityTypeConfiguration
{
	internal class BookEntityConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.OwnerId)
				.IsRequired();

			builder.Property(x => x.Title)
				.IsRequired();

			builder.Property(x => x.Author)
				.IsRequired();

			builder.Property(x => x.Language)
				.IsRequired();

			builder.Property(x => x.Description);

			builder.Property(x => x.Value)
				.IsRequired();

			builder.Property(x => x.Price)
				.IsRequired();
		}
	}
}
