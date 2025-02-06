using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class OrdersWithItemsAndOrderingSpecification : BaseSpecifications<Order>
	{
		public OrdersWithItemsAndOrderingSpecification(string buyerEmail) : base(o => o.BuyerEmail == buyerEmail)
		{
			AddIncludes(o => o.OrderItems);
			AddIncludes(o => o.DeliveryMethod);
			AddOrderByDesc(o => o.OrderDate);

		}

		public OrdersWithItemsAndOrderingSpecification(int id, string buyerEmail) : base(o => o.Id == id && o.BuyerEmail == buyerEmail)
		{
			AddIncludes(o => o.OrderItems);
			AddIncludes(o => o.DeliveryMethod);
		}



	}
}
