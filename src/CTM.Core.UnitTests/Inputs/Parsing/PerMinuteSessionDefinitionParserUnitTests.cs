using CTM.Core.Inputs.Parsing;
using FluentAssertions;
using Xunit;

namespace CTM.Core.UnitTests.Inputs.Parsing
{
    public class PerMinuteSessionDefinitionParserUnitTests
    {
        private const string MatchedSession1 = "Overdoing it in Python 45min";
        private const string MatchedSession2 = "Overdoing it in Python 45MIN";
        private const string UnmatchedSession = "45min Overdoing it in Python";

        [Theory]
        [InlineData(MatchedSession1)]
        [InlineData(MatchedSession2)]
        public void Should_Parse_ReturnTheRightResult_WhenTheInputMatchsCriteria(string input)
        {
            // Given
            var parser = new PerMinuteSessionDefinitionParser();

            // When
            var result = parser.Parse(input);

            // Then
            result.Should().NotBeNull();
            result.IsMatched.Should().Be(true);
            result.SessionDefinition.Should().NotBeNull();
        }

        [Theory]
        [InlineData(UnmatchedSession)]
        [InlineData(MatchedSession1 + "_Suffix")]
        public void Should_Parse_ReturnTheUnmatchedResult_WhenTheInputNotMatchsCriteria(string input)
        {
            // Given
            var parser = new PerMinuteSessionDefinitionParser();

            // When
            var result = parser.Parse(input);

            // Then
            result.Should().NotBeNull();
            result.IsMatched.Should().Be(false);
            result.SessionDefinition.Should().BeNull();
        }
    }
}