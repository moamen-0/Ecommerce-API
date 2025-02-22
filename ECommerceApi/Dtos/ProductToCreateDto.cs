﻿namespace ECommerceApi.Dtos
{
	public class ProductToCreateDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string PictureUrl { get; set; }
		public int ProductTypeId { get; set; }
		public int ProductBrandId { get; set; }
	}
}
