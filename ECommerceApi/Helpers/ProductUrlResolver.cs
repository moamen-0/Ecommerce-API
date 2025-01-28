using AutoMapper;
using Core.Entities;
using ECommerceApi.Dtos;

namespace ECommerceApi.Helpers
{
	public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _configuration; 

		public ProductUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}


		public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
			}
			return string.Empty;
		}
	}
}
