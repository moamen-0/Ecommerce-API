﻿using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T?> GetAsync(int id);
		Task<IReadOnlyList<T>> GetAllWithSpecAsync();
		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
		Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<int> GetCountAsync(ISpecifications<T> spec);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
		//Task<int> SaveChangesAsync();
		Task<IReadOnlyList<T>> ListAllAsync();
		Task<IReadOnlyList<T>>ListAsync(ISpecifications<T> spec);

	}
}
