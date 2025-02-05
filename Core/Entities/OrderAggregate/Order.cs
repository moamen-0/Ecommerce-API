using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
	public class Order : BaseEntity
	{
		public Order()
		{
		}

		public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId)
		{
			OrderItems = orderItems;
			BuyerEmail = buyerEmail;
			ShipToAddress = shipToAddress;
			DeliveryMethod = deliveryMethod;
			Subtotal = subtotal;
			PaymentIntentId = paymentIntentId;
		}

		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
		public Address ShipToAddress { get; set; }
		public DeliveryMethod DeliveryMethod { get; set; }
		public IReadOnlyList<OrderItem> OrderItems { get; set; }
		public decimal Subtotal { get; set; }
		public string PaymentIntentId { get; set; }
		public OrderStatus Status { get; set; } = OrderStatus.Pending;

		public decimal GetTotal()
		{
			return Subtotal + DeliveryMethod.Price;

		}
	}
}
