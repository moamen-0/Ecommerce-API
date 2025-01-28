
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.Product_Specs;
using ECommerceApi.Dtos;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
	public class ProductController : ControllerBase
	{

		//private readonly IProductRepository _repo;
		private readonly IGenericRepository<Product> _repo;
		private readonly IMapper _mapper;

		public ProductController(IGenericRepository<Product> productRepository,IMapper mapper)
		{
			_repo = productRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var spec = new ProductWithBrandAndTypeSpecifications();
			var products = await _repo.GetAllWithSpecAsync(spec);

			if (products == null || products.Count() == 0)
			{
				return NotFound("No products found.");
			}

			return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
		}



		[HttpGet("{id}")]
 		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndTypeSpecifications(id);
			var product = await _repo.GetWithSpecAsync(spec);
			if (product == null)
			{
				return NotFound("Product not found.");
			}
			return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
		}
		[HttpPost]
		public IActionResult Post()
		{
			return Ok("This is the response from the Post method");
		}
		[HttpPut("{id}")]
		public IActionResult Put(int id)
		{
			return Ok($"This is the response from the Put method with id: {id}");
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			return Ok($"This is the response from the Delete method with id: {id}");
		}
	}
}
