using System;
using CTM.Core;
using CTM.Core.Inputs;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Outputs;
using CTM.Core.Outputs.Formatters;
using CTM.Core.Scheduling;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CTM.Bootstrapper.Extensions
{
    public static class ContainerExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services
                .AddInputServices(configuration)
                .AddSchedulingServices(configuration)
                .AddOutputServices();

            return services;
        }

        private static IServiceCollection AddSchedulingServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<SchedulingOptions>(configuration.GetSection("SchedulingOptions"));

            services.AddTransient<ITrackSlotAllocationStrategy, RoundRabinSlotAllocationStrategy>();
            services.AddTransient<ITrackSchedulingProcess, TrackSchedulingProcess>();
            services.AddTransient<ITrackSchedulingEngine, TrackSchedulingEngine>();
            services.AddTransient<ITrackBuilder, TrackBuilder>();

            return services;
        }

        private static IServiceCollection AddInputServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<InputOptions>(m =>
            {
                configuration.GetSection("InputOptions").Bind(m);

                var inputFileFromCmd = configuration["inputfile"];
                if (!string.IsNullOrWhiteSpace(inputFileFromCmd))
                {
                    m.InputFile = inputFileFromCmd;
                }
            });

            services.AddTransient<IFileInputReader, FileInputReader>();
            services.AddTransient<ISessionDefinitionParser, LightningSessionDefinitionParser>();
            services.AddTransient<ISessionDefinitionParser, PerMinuteSessionDefinitionParser>();
            services.AddTransient<ISessionDefinitionReader, SessionDefinitionReader>();

            return services;
        }

        private static IServiceCollection AddOutputServices(this IServiceCollection services)
        {
            // Output formatters
            services.AddTransient<ITrackFormatter, TrackFormatter>();
            services.AddTransient<ITrackSlotFormatter, TrackSlotFormatter>();
            services.AddTransient<ITrackSessionFormatter, TrackSessionFormatter>();

            // Output writers
            services.AddTransient<ITrackOutputWriter, TrackOutputWriter>();
            services.AddTransient<IOutputWriter, ConsoleOutputWriter>();

            return services;
        }
    }
}