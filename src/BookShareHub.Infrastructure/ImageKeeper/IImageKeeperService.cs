namespace BookShareHub.Infrastructure.Interfaces;

public interface IImageKeeperService
{
	Task<string> UploadImageAsync(Stream imageStream, string fileName);
	Task DeleteImageAsync(string imageUrl);
}
