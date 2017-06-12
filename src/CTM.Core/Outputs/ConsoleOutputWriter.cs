using System;

namespace CTM.Core.Outputs
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void Write(string outputText)
        {
            Console.Write(outputText);
        }

        public void WriteLine(string outputText)
        {
            Console.WriteLine(outputText);
        }
    }
}