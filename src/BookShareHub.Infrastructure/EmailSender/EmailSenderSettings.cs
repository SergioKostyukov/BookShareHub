namespace BookShareHub.Infrastructure.Settings;

public class EmailSenderSettings
{
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string SmtpServer { get; set; } = string.Empty;
	public int SmtpPort { get; set; }
	public bool UseSsl { get; set; }
}
