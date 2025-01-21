using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext  _context;

		public ProductRepository(ApplicationDbContext  context)
		{
			_context = context;
		}


		public async Task<Product> GetProductByIdAsync(int id)
		{

			return await _context.Products.FindAsync(id);
		}

		public async Task<Product> GetProductByNameAsync(string name)
		{
			
			return await _context.Products.FindAsync(name);
		}

		public async Task<IReadOnlyList<Product>> GetProductsAsync()
		{

			return await _context.Products.ToListAsync();
		}
	}
}
