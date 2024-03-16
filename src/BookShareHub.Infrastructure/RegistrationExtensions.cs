using BookShareHub.Infrastructure.EmailSender.Interfaces;
using BookShareHub.Infrastructure.ImageKeeper;
using BookShareHub.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShareHub.Infrastructure.EmailSender
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<EmailSettings>(options => configuration.GetSection("EmailSettings").Bind(options));
			services.AddScoped<IEmailSender, EmailSender>();

			services.Configure<ImageKeeperSettings>(options => configuration.GetSection("GoogleCloudStorage").Bind(options));
			services.AddScoped<IImageKeeperService, ImageKeeperService>();

			return services;
		}
	}
}
