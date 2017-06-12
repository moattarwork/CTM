using System;

namespace CTM.Core.Inputs.Parsing
{
    public class ParsingResult
    {
        private ParsingResult()
        {
            IsMatched = false;
        }

        public bool IsMatched { get; private set; }
        public SessionDefinition SessionDefinition { get; private set; }

        public static ParsingResult FromResult(SessionDefinition sessionDefinition)
        {
            if (sessionDefinition == null) throw new ArgumentNullException(nameof(sessionDefinition));

            return new ParsingResult
            {
                IsMatched = true,
                SessionDefinition = sessionDefinition
            };
        }

        public static ParsingResult FromNoMatch()
        {
            return new ParsingResult();
        }
    }
}