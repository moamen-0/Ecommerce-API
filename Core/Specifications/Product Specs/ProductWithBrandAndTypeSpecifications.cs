using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.Product_Specs
{
	public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
	{
		public ProductWithBrandAndTypeSpecifications(ProductSpecParams specParams) : base(
			
			x => 
			(string.IsNullOrEmpty(specParams.Search)) || x.Name.ToLower().Contains(specParams.Search) &&
			(!specParams.BrandId.HasValue || x.ProductBrandId== specParams.BrandId ) &&
			(!specParams.TypeId.HasValue||x.ProductTypeId== specParams.TypeId)

			) {

			Includes.Add(x => x.ProductBrand);
			Includes.Add(x => x.ProductType);

			if (!string.IsNullOrEmpty(specParams.sort))
			{
				switch (specParams.sort)
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

		ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
			


		}
		


		public ProductWithBrandAndTypeSpecifications(int id) : base(x => x.Id == id)
		{
			Includes.Add(x => x.ProductBrand);
			Includes.Add(x => x.ProductType);
		}

	}
}
