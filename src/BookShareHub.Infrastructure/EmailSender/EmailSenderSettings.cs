namespace BookShareHub.Infrastructure.EmailSender;

public class EmailSettings
{
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string SmtpServer { get; set; } = string.Empty;
	public int SmtpPort { get; set; }
	public bool UseSsl { get; set; }
}
