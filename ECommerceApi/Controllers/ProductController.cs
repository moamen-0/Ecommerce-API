
using Core.Entities;
using Core.Interfaces;
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

		public ProductController(IGenericRepository<Product> productRepository)
		{
			_repo = productRepository;

		}

		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetProducts()
		{
			var products = await _repo.GetAllAsync();

			if (products == null || products.Count() == 0)
			{
				return NotFound("No products found.");
			}

			return Ok(products);
		}



		[HttpGet("{id}")]
 		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await _repo.GetAsync(id);
			if (product == null)
			{
				return NotFound("Product not found.");
			}
			return Ok(product);
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
