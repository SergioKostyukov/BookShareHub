namespace BookShareHub.WebApi.DTOs
{
	public class NovaPoshtaSearchSettlementsRequest
	{
		public string apiKey { get; set; }
		public string modelName { get; set; }
		public string calledMethod { get; set; }
		public SettlementsSearchMethodProperties methodProperties { get; set; }
	}

	public class SettlementsSearchMethodProperties
	{
		public string CityName { get; set; }
		public string Limit { get; set; }
		public string Page { get; set; }
	}

}
