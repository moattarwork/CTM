using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CTM.Core.Inputs.Parsing
{
    public class PerMinuteSessionDefinitionParser : ISessionDefinitionParser
    {
        private const string RegExPattern = @"(.+)\s(\d+)(min)$";

        public ParsingResult Parse(string trackInput)
        {
            if (trackInput == null) throw new ArgumentNullException(nameof(trackInput));
            var inputText = trackInput.Trim();

            var regex = new Regex(RegExPattern, RegexOptions.IgnoreCase);
            if (!regex.IsMatch(inputText))
                return ParsingResult.FromNoMatch();

            var matchedSplit = regex.Split(inputText);
            var finalSplit = matchedSplit.Where(m => !string.IsNullOrWhiteSpace(m)).ToArray();

            var title = finalSplit[0];
            var duration = int.Parse(finalSplit[1]);

            var sessionDefinition = new SessionDefinition(title, duration);
            return ParsingResult.FromResult(sessionDefinition);
        }
    }
}