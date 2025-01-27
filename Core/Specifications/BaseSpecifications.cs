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


	}
}
