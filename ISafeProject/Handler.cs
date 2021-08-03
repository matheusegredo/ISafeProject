using ISafeProject.Consumer;
using ISafeProject.Generator;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ISafeProject
{
	public class Handler : IHostedService, IDisposable
	{

		private readonly ITokenGenerator _tokenGenerator;
		private readonly ITokenConsumer _tokenConsumer;

		public Handler(ITokenGenerator tokenGenerator, ITokenConsumer tokenConsumer)
		{
			_tokenGenerator = tokenGenerator;
			_tokenConsumer = tokenConsumer;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var generatorThread = new Thread(_tokenGenerator.Generate);
			generatorThread.Start();

			Thread.Sleep(TimeSpan.FromSeconds(5));

			while (true)
			{
				Console.WriteLine("Enter a token value to login.");
				var token = Console.ReadLine();

				if (_tokenConsumer.Consume(token))
					Console.WriteLine("Success!");
				else
					Console.WriteLine("Login failed");
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			Dispose();
			return Task.CompletedTask;
		}
	}
}
