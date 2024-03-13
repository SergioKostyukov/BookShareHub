using Microsoft.AspNetCore.Http;

namespace BookShareHub.Application.Dto;

public class ImageFileDto 
{
	public IFormFile? ImageFile { get; set; }
}
