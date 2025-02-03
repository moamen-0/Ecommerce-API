using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IBasketService
	{
		Task SetValueAsync(string key, string value);
		Task<string> GetValueAsync(string key);

	}
}
