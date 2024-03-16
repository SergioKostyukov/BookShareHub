using BookShareHub.Infrastructure.ImageKeeper;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookShareHub.Infrastructure.Services;

internal class ImageKeeperService : IImageKeeperService
{
	private readonly ILogger<ImageKeeperService> _logger;
	private readonly IOptions<ImageKeeperSettings> _imageKeeperSettings;
	private readonly StorageClient _storageClient;

	public ImageKeeperService(ILogger<ImageKeeperService> logger, IOptions<ImageKeeperSettings> imageKeeperSettings)
	{
		_logger = logger;
		_imageKeeperSettings = imageKeeperSettings;

		GoogleCredential credential = GoogleCredential.FromFile(_imageKeeperSettings.Value.CredentialFile);
		_storageClient = StorageClient.Create(credential);
	}

	public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
	{
		var objectName = Guid.NewGuid().ToString() + "_" + fileName;

		using (var memoryStream = new MemoryStream())
		{
			await imageStream.CopyToAsync(memoryStream);
			memoryStream.Seek(0, SeekOrigin.Begin);

			await _storageClient.UploadObjectAsync(_imageKeeperSettings.Value.CloudStorageBucket, objectName, null, memoryStream);
		}

		return $"https://storage.googleapis.com/{_imageKeeperSettings.Value.CloudStorageBucket}/{objectName}";
	}

	public async Task DeleteImageAsync(string imageUrl)
	{
		var uri = new Uri(imageUrl);
		var objectName = uri.Segments.Last();

		await _storageClient.DeleteObjectAsync(_imageKeeperSettings.Value.CloudStorageBucket, objectName);
	}
}