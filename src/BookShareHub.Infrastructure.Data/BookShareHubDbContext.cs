using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data.EntityTypeConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShareHub.Infrastructure.Data
{
	public class BookShareHubDbContext : IdentityDbContext<User>
    {
		public BookShareHubDbContext(DbContextOptions<BookShareHubDbContext> options) : base(options) { }
		public BookShareHubDbContext() { }
		public DbSet<User> Users { get; set; }
		public DbSet<ProfileComment> ProfileComments { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderList> OrdersLists { get; set; }
		public DbSet<Chat> Chats { get; set; }
		public DbSet<ChatSubscribersList> ChatsSubscribersLists { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileCommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderListEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChatEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChatSubscribersListEntityConfiguration());

            modelBuilder.Entity<IdentityUserLogin<string>>()
				.HasKey(x => new { x.LoginProvider, x.ProviderKey });
		}
	}
}
