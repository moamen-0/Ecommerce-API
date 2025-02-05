using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
	public class ProductItemOrder
	{
		public ProductItemOrder() { }
		public ProductItemOrder(int productItemId, string productName,string pictureUrl ) {
		
			ProductItemId = productItemId;
			ProductName = productName;
			PictureUrl = pictureUrl;
			}
		
		public int ProductItemId { get; set; }
		public string ProductName { get; set; }
		public string PictureUrl { get; set; }

		



		
	}
}
