using System;
using System.Collections.Generic;
using CTM.Core.Exceptions;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Scheduling;
using CTM.Core.Scheduling.Domain;
using CTM.Core.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace CTM.Core.UnitTests.Scheduling
{
    public class TrackSchedulingEngineUnitTests : IClassFixture<TrackFixture>
    {
        public TrackSchedulingEngineUnitTests(TrackFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly TrackFixture _fixture;

        [Fact]
        public void Should_Calculate_ReturnTheCorrectAllocation_WhenSlotsAreAllocatedCorrectly()
        {
            // Given
            var trackBuilder = Substitute.For<ITrackBuilder>();
            var tracks = new List<Track>{_fixture.EqualSlotsTrack};
            trackBuilder.Build(Arg.Any<int>()).Returns(tracks);

            var optionAccessor = Substitute.For<IOptions<SchedulingOptions>>();
            optionAccessor.Value.Returns(new SchedulingOptions {ConcurrentTracks = 1});

            var strategy = Substitute.For<ITrackSlotAllocationStrategy>();
            var allocationResult = new AllocationResult(new List<TrackSlot>(), new List<SessionDefinition>());
            strategy.Allocate(Arg.Any<IReadOnlyList<TrackSlot>>(), Arg.Any<IReadOnlyList<SessionDefinition>>())
                .Returns(allocationResult);

            var sut = new TrackSchedulingEngine(trackBuilder, strategy, optionAccessor);

            // When
            var result = sut.Calculate(_fixture.SessionDefinitions);

            // Then
            result.ShouldBeEquivalentTo(tracks);
        }

        [Fact]
        public void Should_Calculate_ThrowException_WhenSlotsAreSmallerThenSessions()
        {
            // Given
            var trackBuilder = Substitute.For<ITrackBuilder>();
            trackBuilder.Build(Arg.Any<int>()).Returns(new List<Track>());

            var optionAccessor = Substitute.For<IOptions<SchedulingOptions>>();
            optionAccessor.Value.Returns(new SchedulingOptions {ConcurrentTracks = 1});

            var strategy = Substitute.For<ITrackSlotAllocationStrategy>();
            var allocationResult = new AllocationResult(new List<TrackSlot>(), new List<SessionDefinition> {new SessionDefinition("Session", 30)});
            strategy.Allocate(Arg.Any<IReadOnlyList<TrackSlot>>(), Arg.Any<IReadOnlyList<SessionDefinition>>())
                .Returns(allocationResult);

            var sut = new TrackSchedulingEngine(trackBuilder, strategy, optionAccessor);

            // When
            Action action = () => sut.Calculate(_fixture.SessionDefinitions);

            // Then
            action.ShouldThrow<UnallocatedSessionsException>();
        }

        [Fact]
        public void Should_Calculate_ThrowException_WhenResultIsNotAvailableOrNull()
        {
            // Given
            var trackBuilder = Substitute.For<ITrackBuilder>();
            trackBuilder.Build(Arg.Any<int>()).Returns(new List<Track>());

            var optionAccessor = Substitute.For<IOptions<SchedulingOptions>>();
            optionAccessor.Value.Returns(new SchedulingOptions {ConcurrentTracks = 1});

            var strategy = Substitute.For<ITrackSlotAllocationStrategy>();
            strategy.Allocate(Arg.Any<IReadOnlyList<TrackSlot>>(), Arg.Any<IReadOnlyList<SessionDefinition>>())
                .Returns((AllocationResult)null);

            var sut = new TrackSchedulingEngine(trackBuilder, strategy, optionAccessor);

            // When
            Action action = () => sut.Calculate(_fixture.SessionDefinitions);

            // Then
            action.ShouldThrow<AppException>().WithMessage("Error in allocating sessions. result is null");
        }
    }
}