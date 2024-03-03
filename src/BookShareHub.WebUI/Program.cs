using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using BookShareHub.Application;

namespace BookShareHub;
public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddStorage(builder.Configuration);

		builder.Services.AddDefaultIdentity<User>()
			.AddEntityFrameworkStores<BookShareHubDbContext>();

        builder.Services.AddHttpContextAccessor();

		builder.Services.AddServices();

        builder.Services.AddControllersWithViews();
		builder.Services.AddRazorPages();

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

		app.Run();
	}
}