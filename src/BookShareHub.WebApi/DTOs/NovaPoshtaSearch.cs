namespace BookShareHub.WebApi.DTOs
{
	public class NovaPoshtaRequestBase
	{
		public string apiKey { get; set; }
		public string modelName { get; set; }
		public string calledMethod { get; set; }
		public object methodProperties { get; set; }
	}
}
