using Microsoft.Extensions.Caching.Memory;

namespace ISafeProject.Consumer
{
	public class TokenConsumer : ITokenConsumer
	{
		private readonly IMemoryCache _memoryCache;

		public TokenConsumer(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public bool Consume(string token) => _memoryCache.TryGetValue(token, out string value);		
	}
}
