using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
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

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync()
		{
			//if (typeof(T) == typeof(Product)) {

			//	return (IEnumerable<T>) await _context.Set<Product>()
			//		.Include(p => p.ProductBrand)
			//		.Include(p => p.ProductType)
			//		.ToListAsync();
			//}
			return await _context.Set<T>().AsNoTracking().ToListAsync();

		}
		public async Task<IReadOnlyList<T>> GetAllAsync() {
				
			return await _context.Set<T>().ToListAsync();

		}

		public async Task<T?> GetAsync(int id)
		{
			//if(typeof(T) == typeof(Product))
			//{
			//	return await _context.Set<Product>()
			//		.Where(p => p.Id == id)
			//		.Include(p => p.ProductBrand)
			//		.Include(p => p.ProductType)
			//		.FirstOrDefaultAsync() as T;
			//}

			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecification(spec).AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
		}

		private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
		{
			return SpecificationsEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
		}
	}
}
