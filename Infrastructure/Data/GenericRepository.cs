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
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly ApplicationDbContext _context;

		public GenericRepository(ApplicationDbContext context) 
		{ 
					_context = context;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			if (typeof(T) == typeof(Product)) {

				return (IEnumerable<T>) await _context.Set<Product>()
					.Include(p => p.ProductBrand)
					.Include(p => p.ProductType)
					.ToListAsync();
			}
			return await _context.Set<T>().AsNoTracking().ToListAsync();

		}

		public async Task<T?> GetAsync(int id)
		{
			if(typeof(T) == typeof(Product))
			{
				return await _context.Set<Product>()
					.Where(p => p.Id == id)
					.Include(p => p.ProductBrand)
					.Include(p => p.ProductType)
					.FirstOrDefaultAsync() as T;
			}

			return await _context.Set<T>().FindAsync(id);
		}
	}
}
