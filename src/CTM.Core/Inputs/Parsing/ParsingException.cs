using System;
using System.Collections.Generic;
using System.Text;
using CTM.Core.Exceptions;

namespace CTM.Core.Inputs.Parsing
{
    public class ParsingException : AppException
    {
        public ParsingException(IEnumerable<InvalidParsingResult> invalidResults) : base(BuildMessage(invalidResults))
        {
        }

        public static string BuildMessage(IEnumerable<InvalidParsingResult> invalidResults)
        {
            if (invalidResults == null) throw new ArgumentNullException(nameof(invalidResults));

            var builder =  new StringBuilder("No match to be able to parse:");
            builder.AppendLine();
            foreach (var result in invalidResults)
            {
                builder.AppendLine($"{result.LineNumber} - {result.Input}");
            }

            return builder.ToString();
        }
    }
}