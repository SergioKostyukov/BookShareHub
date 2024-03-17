using BookShareHub.Application;
using BookShareHub.Core.Domain.Entities;
using BookShareHub.Infrastructure;
using BookShareHub.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Razor;

namespace BookShareHub;
public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddLocalization(options =>
		{
			options.ResourcesPath = "Resources";
		});

		// Add services related to storage using the configuration specified
		builder.Services.AddStorage(builder.Configuration);

		// Add default identity services using the User entity and Entity Framework stores
		builder.Services.AddDefaultIdentity<User>()
			.AddEntityFrameworkStores<BookShareHubDbContext>();

		builder.Services.AddHttpContextAccessor();

		builder.Services.AddServices();
		builder.Services.AddBogusServices();
		builder.Services.AddInfrastructure(builder.Configuration);

		builder.Services.AddControllersWithViews()
			.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
			.AddDataAnnotationsLocalization();
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

		app.UseRequestLocalization(options =>
		{
			string[] supportedCultures = ["en-US", "uk-UA"];
			options.SetDefaultCulture(supportedCultures[0])
				.AddSupportedCultures(supportedCultures)
				.AddSupportedUICultures(supportedCultures)
				.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());
		});

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{culture}/{controller=Home}/{action=Index}/{id?}",
			defaults: new { culture = "en-US" }
			);

		app.MapRazorPages();

		_ = app.DatabaseEnsureCreated();

		app.Run();
	}
}