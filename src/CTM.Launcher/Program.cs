using System;
using CTM.Bootstrapper;

namespace CTM.Launcher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var app = AppBuilder.Build(args);
            app.Run();

            Console.ReadKey();
        }
    }
}