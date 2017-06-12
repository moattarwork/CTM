using System;
using System.Linq;
using CTM.Bootstrapper;

namespace CTM.Launcher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (!IsRanWithoutHelpParameter(args))
                return;

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

        private static bool IsRanWithoutHelpParameter(string[] args)
        {
            if (!args.Any(m => m.Equals("--help", StringComparison.CurrentCultureIgnoreCase) ||
                               m.Equals("-h", StringComparison.CurrentCultureIgnoreCase)))
                return true;

            Console.WriteLine("Conference Tracking System");
            Console.WriteLine("Usage: CTM.Launcher [--inputfile <filename>] [--help|-h]");
            Console.WriteLine();
            Console.WriteLine(
                "Runing command without parameter look for the input file in appSettings.json file in InputOptions:InputFile parameter");

            return false;
        }
    }
}