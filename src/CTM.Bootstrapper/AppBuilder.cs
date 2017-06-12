using System;
using CTM.Bootstrapper.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CTM.Bootstrapper
{
    public class AppBuilder
    {
        public static IApplication Build(string[] args)
        {
            var configuration = CreateConfiguration(args);
            var serviceProvider = CreateServiceProvider(configuration);

            var loggerFactory = ConfigureLogger(serviceProvider);
            loggerFactory.AddConsole(LogLevel.Debug);

            return new ConsoleApplication(serviceProvider, loggerFactory);
        }

        private static IConfiguration CreateConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddCommandLine(args);

            return builder.Build();
        }

        private static IServiceProvider CreateServiceProvider(IConfiguration configuration)
        {
            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .AddLogging()
                .AddApplicationServices(configuration)
                .BuildServiceProvider();

            return serviceProvider;
        }

        private static ILoggerFactory ConfigureLogger(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddConsole(LogLevel.Debug);

            return loggerFactory;
        }
    }
}