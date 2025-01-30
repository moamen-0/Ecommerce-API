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
		public ProductWithBrandAndTypeSpecifications(string? sort,int? brandId,int? typeId) : base(
			
			x => 
			(!brandId.HasValue || x.ProductBrandId==brandId  ) && (!typeId.HasValue||x.ProductTypeId==typeId)

			) {

			Includes.Add(x => x.ProductBrand);
			Includes.Add(x => x.ProductType);

			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "priceAsc":
						OrderBy = x => x.Price;

						break;
					case "priceDesc":
						OrderByDesc = x => x.Price;
						break;
					default:
						OrderBy = x => x.Name;
						break;
				}

			}
			else
			{
				OrderBy = x => x.Name;
			}




		}

		public ProductWithBrandAndTypeSpecifications(int id) : base(x => x.Id == id)
		{
			Includes.Add(x => x.ProductBrand);
			Includes.Add(x => x.ProductType);
		}

	}
}
