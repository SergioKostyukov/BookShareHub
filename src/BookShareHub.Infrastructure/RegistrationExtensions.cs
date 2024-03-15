using BookShareHub.Infrastructure.EmailSender.Interfaces;
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

			return services;
		}
	}
}
