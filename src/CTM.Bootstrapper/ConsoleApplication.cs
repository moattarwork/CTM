using System;
using CTM.Bootstrapper.Exceptions;
using CTM.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CTM.Bootstrapper
{
    public class ConsoleApplication : IApplication
    {
        public ConsoleApplication(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory ??
                            throw new ArgumentNullException(nameof(loggerFactory));

            ServiceProvider = serviceProvider ??
                              throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IServiceProvider ServiceProvider { get; }

        public void Run()
        {
            var process = ServiceProvider.GetService<ITrackSchedulingProcess>();
            if (process == null)
                throw new SchedulingProcessNotFoundException();

            process.Run();
        }

        public ILoggerFactory LoggerFactory { get; }
    }
}