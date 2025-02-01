using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.Product_Specs
{
	public class ProductSpecParams
	{
		private const int MaxPageSize = 50;
		public int PageIndex { get; set; } = 1;
		private int _pageSize = 6;
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
		}
		public int? BrandId { get; set; }
		public int? TypeId { get; set; }
		public string? sort { get; set; }
		public int? skip { get; set; }
		public int? take { get; set; }
		public bool InPagination { get; set; }


		//private string? _search;
		//public string? Search
		//{
		//	get => _search;
		//	set => _search = value.ToLower();
		//}
	}
}
