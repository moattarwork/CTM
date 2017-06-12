using System;
using System.Collections.Generic;
using System.Linq;
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
            return $"No match to parse lines {invalidResults.Select(r => r.LineNumber)}";
        }
    }
}