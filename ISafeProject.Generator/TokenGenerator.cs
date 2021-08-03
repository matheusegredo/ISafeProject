using ISafeProject.Generator;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;

namespace ISafeProject.Consumer
{
	public class TokenGenerator : ITokenGenerator
	{
		private readonly IMemoryCache _memoryCache;

		public TokenGenerator(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public void Generate()
		{
			while (true)
			{
				var token = Guid.NewGuid().ToString("N").Substring(0, 5);
				_memoryCache.Set(token, token, TimeSpan.FromSeconds(10));

				Console.WriteLine($"New token genareted at: {DateTime.Now}");
				Console.WriteLine($"Value: {token}");

				Thread.Sleep(TimeSpan.FromSeconds(10));
			}
		}
	}
}
