using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	internal static class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity> specifications)
		{
			var query = inputQuery;
			if (specifications.Criteria != null)
			{
				query = query.Where(specifications.Criteria);
			}
			if (specifications.OrderBy != null)
			{
				query = query.OrderBy(specifications.OrderBy);
			}
			if (specifications.OrderByDesc != null)
			{
				query = query.OrderByDescending(specifications.OrderByDesc);
			}
			query = specifications.Includes.Aggregate(query, (current, include) => current.Include(include));
			return query;
		}
	}
}
