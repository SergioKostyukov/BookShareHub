using BookShareHub.Infrastructure.Interfaces;
using BookShareHub.Infrastructure.Services;
using BookShareHub.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShareHub.Infrastructure
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<EmailSenderSettings>(options => configuration.GetSection("EmailSettings").Bind(options));
			services.AddScoped<IEmailSender, EmailSender>();

			services.Configure<ImageKeeperSettings>(options => configuration.GetSection("GoogleCloudStorage").Bind(options));
			services.AddScoped<IImageKeeperService, ImageKeeperService>();

			return services;
		}
	}
}
