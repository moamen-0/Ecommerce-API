namespace ECommerceApi.Dtos
{
	public class ProductToUpdateDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string PictureUrl { get; set; }
		public int ProductTypeId { get; set; }
		public int ProductBrandId { get; set; }
		public string ProductName { get; set; }
		public string ProductBrand { get; set; }
		public string ProductType { get; set; }
	


	}
}
