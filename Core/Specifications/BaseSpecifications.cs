using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
	{
		public BaseSpecifications() {
		
			}
		public BaseSpecifications(Expression<Func<T, bool>> CriteriaExpression)
		{
			Criteria = CriteriaExpression;
			

		}
		public Expression<Func<T, bool>> Criteria { get; set; }
		public List<Expression<Func<T, object>>> Includes { get; set; }= new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDesc { get; set; }
		public int Skip { get; set; }
		public int Take { get; set; }
		public bool IsPagination { get; set; }

		public void ApplyPagination(int skip,int take) {

			IsPagination = true;
			Skip = skip;
			Take = take;

		}

		//public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
		//{
		//	OrderBy = orderByExpression;
		//}

		//public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
		//{
		//	OrderByDesc = orderByDescExpression;
		//}
		public void AddIncludes(Expression<Func<T, object>> includeExpression)
		{
			Includes.Add(includeExpression);
		}
		public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
		{
			OrderByDesc = orderByDescExpression;
		}
	}
}
