using System.Collections.Generic;
using CTM.Core.Inputs;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Outputs;
using CTM.Core.Scheduling;
using CTM.Core.Scheduling.Domain;
using NSubstitute;
using Xunit;

namespace CTM.Core.UnitTests
{
    public class TrackSchedulingProcessUnitTests
    {
        private List<SessionDefinition> Definitions => new List<SessionDefinition>
        {
            new SessionDefinition("Title #1", 60),
            new SessionDefinition("Title #2", 45),
            new SessionDefinition("Title #2", 5)
        };

        private List<Track> Tracks => new List<Track>
        {
            new Track(1, new List<TrackSlot>()),
            new Track(2, new List<TrackSlot>())
        };

        [Fact]
        public void Should_Run_RetrunTheSameNumberOfTracks()
        {
            // Given
            var sessionDefinitionReader = Substitute.For<ISessionDefinitionReader>();

            var definitions = Definitions;
            sessionDefinitionReader.ReadDefinition().Returns(definitions);

            var trackSchedulingEngine = Substitute.For<ITrackSchedulingEngine>();
            var tracks = Tracks;
            trackSchedulingEngine.Calculate(Arg.Any<List<SessionDefinition>>()).Returns(tracks);

            var trackOutputWriter = Substitute.For<ITrackOutputWriter>();

            var sut = new TrackSchedulingProcess(sessionDefinitionReader, trackSchedulingEngine, trackOutputWriter);

            // When
            sut.Run();

            // Then
            trackSchedulingEngine.Received(1).Calculate(definitions);
            trackOutputWriter.Received(1).Write(tracks[0]);
            trackOutputWriter.Received(1).Write(tracks[1]);
        }
    }
}