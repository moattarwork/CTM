using CTM.Core.Inputs.Parsing;
using FluentAssertions;
using Xunit;

namespace CTM.Core.UnitTests.Inputs.Parsing
{
    public class ParsingResultUnitTests
    {
        [Fact]
        public void Should_FromNoMatch_ReturnFalseMatchFlagAndResult()
        {
            // Given

            // When
            var result = ParsingResult.FromNoMatch();

            // Then
            result.Should().NotBeNull();
            result.IsMatched.Should().Be(false);
            result.SessionDefinition.Should().BeNull();
        }

        [Fact]
        public void Should_FromResult_ReturnTrueMatchFlagAndResult()
        {
            // Given
            var sessionDefinition = new SessionDefinition("Title", 15);

            // When
            var result = ParsingResult.FromResult(sessionDefinition);

            // Then
            result.Should().NotBeNull();
            result.IsMatched.Should().Be(true);
            result.SessionDefinition.ShouldBeEquivalentTo(sessionDefinition);
        }
    }
}