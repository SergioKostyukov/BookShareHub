namespace BookShareHub.WebUI.Models
{
	public class DeliveryParams
	{
		public string DeliveryUserFullName { get; set; } = string.Empty;
		public string DeliveryUserPhoneNumber { get; set; } = string.Empty;
		public string DeliveryCityShortAddress { get; set; } = string.Empty;
		//public bool IsDeliveryCityAddressChosen { get; set; } = false;
		public string DeliveryCityFullAddress { get; set; } = string.Empty;
		public string DeliverySpecificAddress { get; set; } = string.Empty;
		//public bool IsDeliverySpecificAddressChosen { get; set; } = false;
	}
}
