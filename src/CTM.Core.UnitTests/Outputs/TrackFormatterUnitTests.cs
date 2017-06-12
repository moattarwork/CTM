using System;
using System.Collections.Generic;
using CTM.Core.Outputs.Formatters;
using CTM.Core.Scheduling.Domain;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CTM.Core.UnitTests.Outputs
{
    public class TrackFormatterUnitTests
    {
        [Fact]
        public void Should_Format_ReturnCorrectFormattedOutput()
        {
            // Given
            var slot = new TrackSlot("Lunch", new TimeSlot(12, 0, 60), true);
            var track = new Track(1, new List<TrackSlot> {slot});

            var trackSlotFormatter = Substitute.For<ITrackSlotFormatter>();
            trackSlotFormatter.Format(Arg.Any<TrackSlot>()).Returns("12:00PM Lunch");

            var sut = new TrackFormatter(trackSlotFormatter);

            // When
            var result = sut.Format(track);

            // Then
            result.Should().Be("Track #1" +
                               Environment.NewLine +
                               Environment.NewLine +
                               "12:00PM Lunch");
        }
    }
}