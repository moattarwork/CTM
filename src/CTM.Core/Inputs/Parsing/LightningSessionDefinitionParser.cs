using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CTM.Core.Inputs.Parsing
{
    public class LightningSessionDefinitionParser : ISessionDefinitionParser
    {
        private const string RegExPattern = @"(.+)\s(lightning)$";

        public ParsingResult Parse(string trackInput)
        {
            if (trackInput == null) throw new ArgumentNullException(nameof(trackInput));

            var regex = new Regex(RegExPattern, RegexOptions.IgnoreCase);
            var inputText = trackInput.Trim();

            if (!regex.IsMatch(inputText))
                return ParsingResult.FromNoMatch();

            var matchedSplit = regex.Split(inputText);
            var finalSplit = matchedSplit.Where(m => !string.IsNullOrWhiteSpace(m)).ToArray();
            var title = finalSplit[0];
            var duration = 5;

            var sessionDefinition = new SessionDefinition(title, duration);
            return ParsingResult.FromResult(sessionDefinition);
        }
    }
}