using CTM.Core.Inputs.Parsing;
using CTM.Core.Scheduling;
using CTM.Core.Scheduling.Domain;
using FluentAssertions;
using Xunit;

namespace CTM.Core.UnitTests.Scheduling
{
    public class CalculatedTrackSlotUnitTests
    {
        [Fact]
        public void Should_AllocateSession_AllocateTheSessionBasedOnTheLastSessionAndCalculateRemainigCorrectly()
        {
            // Given
            var time = new Slot(9, 0, 45);

            var trackSlot = new TrackSlot("Title", new Slot(9, 0, 120));
            trackSlot.TrackSessions.Add(new TrackSession("Session 0", time));

            var calculatedTrackSlot = new CalculatedTrackSlot(trackSlot);
            var sessionDefinition = new SessionDefinition("Session 1", 60);

            // When
            calculatedTrackSlot.AllocateSession(sessionDefinition);

            // Then
            trackSlot.TrackSessions
                .Should().HaveCount(2);

            trackSlot.TrackSessions[1].Title.Should().Be(sessionDefinition.Title);
            trackSlot.TrackSessions[1].Time.DurationInMinute.Should().Be(sessionDefinition.Duration);
            trackSlot.TrackSessions[1].Time.Hour.Should().Be(9);
            trackSlot.TrackSessions[1].Time.Minute.Should().Be(45);
        }

        [Fact]
        public void Should_AllocateSession_AllocateTheSessionBasedOnTheSlotAndCalculateRemainigCorrectly()
        {
            // Given
            var trackSlot = new TrackSlot("Title", new Slot(9, 0, 120));
            var calculatedTrackSlot = new CalculatedTrackSlot(trackSlot);

            var sessionDefinition = new SessionDefinition("Session 1", 60);

            // When
            calculatedTrackSlot.AllocateSession(sessionDefinition);

            // Then
            trackSlot.TrackSessions
                .Should().HaveCount(1);

            trackSlot.TrackSessions[0].Title.Should().Be(sessionDefinition.Title);
            trackSlot.TrackSessions[0].Time.DurationInMinute.Should().Be(sessionDefinition.Duration);
            trackSlot.TrackSessions[0].Time.Hour.Should().Be(trackSlot.Slot.Hour);
            trackSlot.TrackSessions[0].Time.Minute.Should().Be(trackSlot.Slot.Minute);
        }
    }
}