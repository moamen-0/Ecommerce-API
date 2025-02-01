using ECommerceApi.Dtos;

namespace ECommerceApi.Helpers
{
	public class Pagination<T>
	{
		

		public Pagination(int pageIndex, int pageSize,int count, IReadOnlyList<T> data)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			Data = data;
			Count = count;
		}

		public int PageIndex { get; set; }
		public int Count { get; set; }
		public int PageSize { get; set; }

		public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

		public IReadOnlyCollection<T> Data { get; set; } = new List<T>();
	}
}
