namespace BookShareHub.WebUI.Models
{
	public class BookTitleDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		//public string? FilePath { get; set; }
	}

	public class BookDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string Language { get; set; }
		public string? Description { get; set; }
		//public string? ImagePath { get; set; }
		//public decimal? OriginalPrice { get; set; }
		public decimal? Price { get; set; }
	}
}
