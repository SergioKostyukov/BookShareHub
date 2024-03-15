namespace BookShareHub.Infrastructure.EmailSender.Interfaces
{
	public interface IEmailSender
	{
		void SendEmail(string receiver, string receiverName, string subject, string body);
	}
}
