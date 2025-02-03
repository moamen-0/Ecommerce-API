using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public class BasketService : IBasketService 
	{
		private readonly IConnectionMultiplexer _redis;
		public BasketService(IConnectionMultiplexer redis)
		{
			_redis = redis;
		}
		public async Task SetValueAsync(string key, string value)
		{
			var db = _redis.GetDatabase();
			await db.StringSetAsync(key, value);
		}
		public async Task<string> GetValueAsync(string key)
		{
			var db = _redis.GetDatabase();
			return await db.StringGetAsync(key);
		}
	}
}
