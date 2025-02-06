using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
	public class UnitOfWork : IUnitOfWork 
	{
		private readonly ApplicationDbContext _context;
		private Hashtable _repositories;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}
		public void Dispose()
		{
			_context.Dispose();
		}

		public async Task<int> Complete()
		{
			return await _context.SaveChangesAsync();
		}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			if (_repositories == null) _repositories = new Hashtable();

			var type = typeof(TEntity).Name;

			if (!_repositories.ContainsKey(type))
			{
				var repositoryType = typeof(GenericRepository<>);
				var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
				_repositories.Add(type, repositoryInstance);
			}

			return (IGenericRepository<TEntity>)_repositories[type];

			
		}



		}
}
