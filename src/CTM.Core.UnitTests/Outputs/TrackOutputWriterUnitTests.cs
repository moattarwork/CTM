using CTM.Core.Outputs;
using CTM.Core.Outputs.Formatters;
using CTM.Core.Scheduling.Domain;
using CTM.Core.UnitTests.Fixtures;
using NSubstitute;
using Xunit;

namespace CTM.Core.UnitTests.Outputs
{
    public class TrackOutputWriterUnitTests : IClassFixture<TrackFixture>
    {
        public TrackOutputWriterUnitTests(TrackFixture trackFixture)
        {
            _trackFixture = trackFixture;
        }

        private readonly TrackFixture _trackFixture;

        [Fact]
        public void Should_Write_WriteTheCorrectMessageToTheOutput()
        {
            // Given
            var outputWriter = Substitute.For<IOutputWriter>();
            var trackFormatter = Substitute.For<ITrackFormatter>();
            trackFormatter.Format(Arg.Any<Track>()).Returns("Formatted Track");

            var sut = new TrackOutputWriter(trackFormatter, outputWriter);

            // When
            sut.Write(_trackFixture.EqualSlotsTrack);

            // Then
            outputWriter.Received(1).WriteLine(Arg.Is<string>(m => m == "Formatted Track"));
        }
    }
}