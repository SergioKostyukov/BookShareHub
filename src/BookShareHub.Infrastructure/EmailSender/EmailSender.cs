using BookShareHub.Infrastructure.EmailSender.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BookShareHub.Infrastructure.EmailSender
{
	internal class EmailSender(ILogger<EmailSender> logger, IOptions<EmailSettings> emailSettings) : IEmailSender
	{
		private readonly ILogger<EmailSender> _logger = logger;
		private readonly IOptions<EmailSettings> _emailSettings = emailSettings;

		public void SendEmail(string receiver, string receiverName, string subject, string body)
		{
			try
			{
				var email = new MimeMessage();

				email.From.Add(new MailboxAddress("BookShareHub", _emailSettings.Value.Email));
				email.To.Add(new MailboxAddress(receiverName, "kostyukov.serg2003@gmail.com"/*receiver*/));

				email.Subject = subject;
				email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
				{
					Text = body
				};

				using (var smtp = new SmtpClient())
				{
					smtp.Connect(_emailSettings.Value.SmtpServer, _emailSettings.Value.SmtpPort, _emailSettings.Value.UseSsl);

					smtp.Authenticate(_emailSettings.Value.Email, _emailSettings.Value.Password);

					smtp.Send(email);
					smtp.Disconnect(true);
					_logger.LogInformation("Email send success");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("Email send error! {0}", ex.Message);
			}
		}
	}
}