using System;
using CTM.Core.Outputs.Formatters;
using CTM.Core.Scheduling.Domain;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CTM.Core.UnitTests.Outputs
{
    public class TrackSlotFormatterUnitTests
    {
        [Fact]
        public void Should_Format_ReturnCorrectFormattedOutput_WhenSlotHasSessions()
        {
            // Given
            var slot = new TrackSlot("Morning", new TimeSlot(9, 0, 180));
            var session = new TrackSession("Session #1", new TimeSlot(11, 15, 45));
            slot.TrackSessions.Add(session);

            var trackSessionFormatter = Substitute.For<ITrackSessionFormatter>();

            trackSessionFormatter.Format(Arg.Any<TrackSession>()).Returns("11:15AM Session #1");

            var sut = new TrackSlotFormatter(trackSessionFormatter);

            // When
            var result = sut.Format(slot);

            // Then
            result.Should().Be("11:15AM Session #1" + Environment.NewLine);
        }

        [Fact]
        public void Should_Format_ReturnCorrectFormattedOutput_WhenSlotIsPreScheduled()
        {
            // Given
            var slot = new TrackSlot("Lunch", new TimeSlot(12, 0, 60), true);
            var trackSessionFormatter = Substitute.For<ITrackSessionFormatter>();

            var sut = new TrackSlotFormatter(trackSessionFormatter);

            // When
            var result = sut.Format(slot);

            // Then
            result.Should().Be("12:00PM Lunch " + Environment.NewLine);
        }
    }
}