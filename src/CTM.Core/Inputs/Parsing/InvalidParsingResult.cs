using System;

namespace CTM.Core.Inputs.Parsing
{
    public class InvalidParsingResult
    {
        public InvalidParsingResult(int lineNumber, string input)
        {
            if (lineNumber <= 0) throw new ArgumentOutOfRangeException(nameof(lineNumber));

            LineNumber = lineNumber;
            Input = input ?? throw new ArgumentNullException(nameof(input));
        }

        public int LineNumber { get; }
        public string Input { get; }
    }
}