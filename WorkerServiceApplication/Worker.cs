using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace WorkerServiceApplication
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;

		public Worker(ILogger<Worker> logger)
		{
			_logger = logger;
		}

		public override Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Service started at: {time}", DateTimeOffset.Now);
			return base.StartAsync(cancellationToken);
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Service stoped at: {time}", DateTimeOffset.Now);

			return base.StopAsync(cancellationToken);	
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				if (GetResponse())
				{
					_logger.LogInformation("Response is OK: {time}", DateTimeOffset.Now);
				}
				else
				{
					_logger.LogInformation("No Response: {time}", DateTimeOffset.Now);
				}
				await Task.Delay(3000, cancellationToken);
			}
		}

		public bool GetResponse()
		{
			return true;
		}
	}
}
