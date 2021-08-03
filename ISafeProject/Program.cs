using ISafeProject.Consumer;
using ISafeProject.Generator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ISafeProject
{
	public class Program
	{
		private async static Task Main()
		{
			IHostBuilder host = null;

			try
			{
				host = new HostBuilder()
					.UseContentRoot(Directory.GetCurrentDirectory())
					.ConfigureServices((hostContext, services) =>
					{
						services.AddMemoryCache();
						services.AddSingleton<IHostedService, Handler>();
						services.AddSingleton<ITokenGenerator, TokenGenerator>();
						services.AddScoped<ITokenConsumer, TokenConsumer>();
					});
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error injecting services. Message: {ex.Message} Exception: {ex}");
			}
			finally
			{
				host.UseConsoleLifetime();
				await host.RunConsoleAsync();
			}
		}
	}
}
