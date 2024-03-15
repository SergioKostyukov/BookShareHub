namespace BookShareHub.Infrastructure.EmailSender;

public class EmailSettings
{
	public string Email { get; set; }
	public string Password { get; set; }
	public string SmtpServer { get; set; }
	public int SmtpPort { get; set; }
	public bool UseSsl { get; set; }
}
