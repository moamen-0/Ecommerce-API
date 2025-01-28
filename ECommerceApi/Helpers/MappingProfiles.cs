using AutoMapper;

namespace ECommerceApi.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Core.Entities.Product, Dtos.ProductToReturnDto>()
				.ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
				.ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
		}

	}
}
