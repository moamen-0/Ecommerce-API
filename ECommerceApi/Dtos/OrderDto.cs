namespace ECommerceApi.Dtos
{
	public class OrderDto
	{
		public string BuyerEmail { get; set; }
		public AddressDto ShipToAddress { get; set; }
		public int DeliveryMethodId { get; set; }
		public string BasketId { get; set; }
		public decimal Subtotal { get; set; }
	}
}
