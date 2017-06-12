using System.Collections.Generic;
using CTM.Core.Scheduling;
using CTM.Core.Scheduling.Domain;
using CTM.Core.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace CTM.Core.UnitTests.Scheduling
{
    public class TrackSchedulingEngineUnitTestsv : IClassFixture<TrackFixture>
    {
        public TrackSchedulingEngineUnitTestsv(TrackFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly TrackFixture _fixture;

        [Fact]
        public void Should_Calculate_ReturnTheCorrectAllocation_WhenSlotsAreEqual()
        {
            // Given
            var trackBuilder = Substitute.For<ITrackBuilder>();
            trackBuilder.Build(Arg.Any<int>()).Returns(new List<Track> {_fixture.EqualSlotsTrack});

            var optionAccessor = Substitute.For<IOptions<SchedulingOptions>>();
            optionAccessor.Value.Returns(new SchedulingOptions {ConcurrentTracks = 1});

            var sut = new TrackSchedulingEngine(trackBuilder, optionAccessor);

            // When
            var result = sut.Calculate(_fixture.SessionDefinitions);

            // Then
            result[0].Slots[0].TrackSessions.Should()
                .HaveCount(2)
                .And.Contain(m => m.Title == "Session #2")
                .And.Contain(m => m.Title == "Session #3");

            result[0].Slots[1].TrackSessions.Should()
                .HaveCount(3)
                .And.Contain(m => m.Title == "Session #5")
                .And.Contain(m => m.Title == "Session #1")
                .And.Contain(m => m.Title == "Session #4");
        }

        [Fact]
        public void Should_Calculate_ReturnTheCorrectAllocation_WhenSlotsAreUnequal()
        {
            // Given
            var trackBuilder = Substitute.For<ITrackBuilder>();
            trackBuilder.Build(Arg.Any<int>()).Returns(new List<Track> {_fixture.UnequalSlotsTrack});

            var optionAccessor = Substitute.For<IOptions<SchedulingOptions>>();
            optionAccessor.Value.Returns(new SchedulingOptions {ConcurrentTracks = 1});

            var sut = new TrackSchedulingEngine(trackBuilder, optionAccessor);

            // When
            var result = sut.Calculate(_fixture.SessionDefinitions);

            // Then
            result[0].Slots[0].TrackSessions.Should()
                .HaveCount(2)
                .And.Contain(m => m.Title == "Session #5")
                .And.Contain(m => m.Title == "Session #1");

            result[0].Slots[1].TrackSessions.Should()
                .HaveCount(3)
                .And.Contain(m => m.Title == "Session #3")
                .And.Contain(m => m.Title == "Session #2")
                .And.Contain(m => m.Title == "Session #4");
        }
    }
}