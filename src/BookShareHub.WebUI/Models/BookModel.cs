﻿using BookShareHub.Application.Dto;

namespace BookShareHub.WebUI.Models;

public class BookModel
{
	public required BookDto Book { get; init; }

	public IFormFile? ImageFile { get; set; }
}
