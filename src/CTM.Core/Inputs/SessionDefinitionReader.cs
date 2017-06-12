using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Exceptions;
using CTM.Core.Inputs.Parsing;
using Microsoft.Extensions.Options;

namespace CTM.Core.Inputs
{
    public class SessionDefinitionReader : ISessionDefinitionReader
    {
        private readonly IFileInputReader _fileInputReader;
        private readonly InputOptions _inputOptions;
        private readonly IEnumerable<ISessionDefinitionParser> _parsers;

        public SessionDefinitionReader(
            IFileInputReader fileInputReader,
            IOptions<InputOptions> optionAccessor,
            IEnumerable<ISessionDefinitionParser> parsers)
        {
            if (optionAccessor == null) throw new ArgumentNullException(nameof(optionAccessor));
            _fileInputReader = fileInputReader ?? throw new ArgumentNullException(nameof(fileInputReader));
            _parsers = parsers ?? throw new ArgumentNullException(nameof(parsers));

            _inputOptions = optionAccessor.Value;
        }

        public IReadOnlyList<SessionDefinition> ReadDefinition()
        {
            var inputs = _fileInputReader.ReadContent(_inputOptions.InputFile);
            if (inputs == null)
                throw new AppException($"Error in reading input from the file {_inputOptions.InputFile}");

            var sessionDefinitions = new List<SessionDefinition>();
            var invalidParsingResults = new List<InvalidParsingResult>();

            for (var index = 0; index < inputs.Length; index++)
            {
                var input = inputs[index];
                var isMatched = false;

                foreach (var parser in _parsers)
                {
                    var result = parser.Parse(input);
                    if (!result.IsMatched)
                        continue;

                    sessionDefinitions.Add(result.SessionDefinition);
                    isMatched = true;
                    break;
                }

                if (isMatched == false)
                    invalidParsingResults.Add(new InvalidParsingResult(index + 1, input));

                if (invalidParsingResults.Any())
                    throw new ParsingException(invalidParsingResults);
            }

            return sessionDefinitions;
        }
    }
}