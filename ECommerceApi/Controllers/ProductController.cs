
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.Product_Specs;
using ECommerceApi.Dtos;
using ECommerceApi.Helpers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ProductController : ControllerBase
	{

		//private readonly IProductRepository _repo;
		private readonly IGenericRepository<Product> _repo;
		private readonly IGenericRepository<ProductBrand> _brandRepo;
		private readonly IGenericRepository<ProductType> _typeRepo;
		private readonly IMapper _mapper;

		public ProductController(
			IGenericRepository<Product> productRepository,
			IMapper mapper,
			IGenericRepository<ProductBrand> brandRepo,
			IGenericRepository<ProductType> typeRepo)
		{
			_repo = productRepository;
			_mapper = mapper;
			_brandRepo = brandRepo;
			_typeRepo = typeRepo;
		}
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
		{
			var spec = new ProductWithBrandAndTypeSpecifications(specParams);
			var products = await _repo.GetAllWithSpecAsync(spec);

			if (products == null || products.Count() == 0)
			{
				return NotFound("No products found.");
			}

			var countSpec = new ProductCountSpecifications(specParams);
			var count = await _repo.GetCountAsync(countSpec);

			var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

			return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize,count,data));
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


		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
		{
			var brands = await _brandRepo.GetAllAsync();
			
			if (brands == null || brands.Count() == 0)
			{
				return NotFound("No brands found.");
			}
			return Ok(brands);
		}


		[HttpGet("types")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
		{
			var types = await _typeRepo.GetAllWithSpecAsync();
			if (types == null || types.Count() == 0)
			{
				return NotFound("No types found.");
			}
			return Ok(types);
		}

		[HttpPost]
		public async Task<ActionResult<ProductToReturnDto>> PostProduct(ProductToCreateDto productToCreateDto)
		{
			var product = _mapper.Map<ProductToCreateDto, Product>(productToCreateDto);
			_repo.Add(product);
			//_repo.SaveChangesAsync();
			return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> PutProduct(int id, ProductToUpdateDto productToUpdateDto)
		{
			var product = await _repo.GetAsync(id);
			if (product == null)
			{
				return NotFound("Product not found.");
			}
			_mapper.Map(productToUpdateDto, product);
			_repo.Update(product);
			//_repo.SaveChangesAsync();
			return Ok(product);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var product = _repo.GetAsync(id).Result;
			if (product == null)
			{
				return NotFound("Product not found.");
			}
			_repo.Delete(product);
			//_repo.SaveChangesAsync();
			return Ok();

		}
	}
}
