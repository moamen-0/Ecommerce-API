using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
	public class DeliveryMethod : BaseEntity
	{
		public DeliveryMethod() { }

		public string ShortName { get; set; }
		public string ShortDescription { get; set; }
		public string DeliveryTime { get; set; }
		public decimal Price {  get; set; }
		


	}
}
