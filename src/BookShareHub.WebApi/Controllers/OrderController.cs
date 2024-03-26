using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using BookShareHub.WebApi.DTOs;

namespace BookShareHub.WebApi.Controllers
{
	public class OrderController(IHttpClientFactory clientFactory) : Controller
	{
		private readonly IHttpClientFactory _clientFactory = clientFactory;

		[HttpPost]
		[Route("searchSettlements")]
		public async Task<IActionResult> SearchSettlements([FromBody] NovaPoshtaSearchSettlementsRequest request)
		{
			var client = _clientFactory.CreateClient();

			var jsonRequest = JsonSerializer.Serialize(request);
			var stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

			var response = await client.PostAsync("https://api.novaposhta.ua/v2.0/json/", stringContent);

			if (response.IsSuccessStatusCode)
			{
				var jsonResponse = await response.Content.ReadAsStringAsync();
				return Ok(jsonResponse);
			}
			else
			{
				return StatusCode((int)response.StatusCode, "Failed to retrieve data from Nova Poshta API");
			}
		}
	}
}
