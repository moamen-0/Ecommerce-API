using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
	public class OrderItem : BaseEntity
	{
		public OrderItem() { }
		public OrderItem(ProductItemOrder ItemOrder,decimal price,int quantity)
		{
			ProductItemOrder = ItemOrder;
			Price = price;
			Quantity = quantity;
		}
		public ProductItemOrder ProductItemOrder { get; set; }
		
		public decimal Price { get; set; }
		public int Quantity { get; set; }

	}
}
