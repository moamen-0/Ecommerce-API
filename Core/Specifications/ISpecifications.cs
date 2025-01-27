using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public interface ISpecifications<T> where T : BaseEntity
	{
		
		public Expression<Func<T, bool>> Criteria { get; set; }
		
		public List<Expression<Func<T, object>>> Includes { get; set; }

	}
}
