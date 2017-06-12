using System;
using Microsoft.Extensions.Logging;

namespace CTM.Bootstrapper
{
    public interface IApplication
    {
        IServiceProvider ServiceProvider { get; }
        ILoggerFactory LoggerFactory { get; }

        void Run();
    }
}