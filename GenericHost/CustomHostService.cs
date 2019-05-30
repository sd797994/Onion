using Autofac;
using Infrastructure.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHost
{


    public class CustomHostService : IHostedService
    {
        private Task _executingTask;
        private readonly ILogger<ConsoleLoggerProvider> _logger;
        public CustomHostService(ILogger<ConsoleLoggerProvider> logger,  ILifetimeScope container)
        {
            IocContainer.BuilderIocContainer(container);
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(CustomHostService)}start");
            _executingTask = ExecuteAsync();
            if (_executingTask.IsCompleted)
            {
                await _executingTask;
            }
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
        }

        public async Task ExecuteAsync()
        {
            _logger.LogInformation($"{nameof(CustomHostService):executing}");
            await Task.Delay(5000);
        }
    }
}
