using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.Product_Specs
{
	public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
	{
		public ProductWithBrandAndTypeSpecifications() : base() {

			Includes.Add(x => x.ProductBrand);
			Includes.Add(x => x.ProductType);
		}

		public ProductWithBrandAndTypeSpecifications(int id) : base(x => x.Id == id)
		{
			Includes.Add(x => x.ProductBrand);
			Includes.Add(x => x.ProductType);
		}

	}
}
