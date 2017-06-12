using System;
using CTM.Bootstrapper;

namespace CTM.Launcher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var app = AppBuilder.Build(args);
                app.Run();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}