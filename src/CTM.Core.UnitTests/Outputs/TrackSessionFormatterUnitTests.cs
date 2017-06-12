using CTM.Core.Outputs.Formatters;
using CTM.Core.Scheduling.Domain;
using FluentAssertions;
using Xunit;

namespace CTM.Core.UnitTests.Outputs
{
    public class TrackSessionFormatterUnitTests
    {
        [Fact]
        public void Should_Format_ReturnCorrectFormattedOutput()
        {
            // Given
            var session = new TrackSession("Session #1", new Slot(11, 15, 45));
            var sut = new TrackSessionFormatter();

            // When
            var result = sut.Format(session);

            // Then
            result.Should().Be("11:15AM Session #1");
        }
    }
}