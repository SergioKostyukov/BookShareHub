using BookShareHub.Application;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using BookShareHub.Infrastructure.EmailSender;
using Microsoft.AspNetCore.Identity;

namespace BookShareHub;
public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services related to storage using the configuration specified
		builder.Services.AddStorage(builder.Configuration);

		// Add default identity services using the User entity and Entity Framework stores
		builder.Services.AddDefaultIdentity<User>()
			.AddEntityFrameworkStores<BookShareHubDbContext>();

		builder.Services.AddHttpContextAccessor();

		builder.Services.AddServices();
		builder.Services.AddBogusServices();
		builder.Services.AddInfrastructure(builder.Configuration);

		builder.Services.AddControllersWithViews();
		builder.Services.AddRazorPages();

		// Configure identity options to customize password requirements
		builder.Services.Configure<IdentityOptions>(options =>
		{
			options.Password.RequireDigit = false;
			options.Password.RequireUppercase = false;
			options.Password.RequireNonAlphanumeric = false;
		});

		var app = builder.Build();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.MapRazorPages();

		app.DatabaseEnsureCreated();

		app.Run();
	}
}