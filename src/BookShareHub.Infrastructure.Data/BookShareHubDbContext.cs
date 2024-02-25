using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BookShareHub.Core.Domain.Entities;

namespace BookShareHub.Infrastructure.Data
{
	public class BookShareHubDbContext : DbContext
	{
		public BookShareHubDbContext(DbContextOptions options) : base(options) { }

		public BookShareHubDbContext() { }
		public DbSet<User> Users { get; set; }
		public DbSet<ProfileComment> ProfileComments { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderList> OrdersLists { get; set; }
		public DbSet<Chat> Chats { get; set; }
		public DbSet<ChatSubscribersList> ChatsSubscribersLists { get; set; }
	}
}
