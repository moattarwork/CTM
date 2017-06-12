using CTM.Core.Scheduling;
using FluentAssertions;
using Xunit;

namespace CTM.Core.UnitTests.Scheduling
{
    public class TrackBuilderUnitTests
    {
        [Fact]
        public void Should_Build_ReturnTheRigthTemplateForTrack()
        {
            // Given
            var sut = new TrackBuilder();

            // When
            var result = sut.Build(2);

            // Then
            result.Should().NotBeNull()
                .And.HaveCount(2);

            result[0].Slots.Should()
                .HaveCount(4)
                .And.Contain(m => m.Title == "Morning Sessions" && m.Slot.DurationInMinute == 180 &&
                                  m.IsPreScheduled == false)
                .And.Contain(m => m.Title == "Lunch" && m.Slot.DurationInMinute == 60 && m.IsPreScheduled)
                .And.Contain(m => m.Title == "Afternoon Sessions" && m.Slot.DurationInMinute == 240 &&
                                  m.IsPreScheduled == false)
                .And.Contain(m => m.Title == "Networking" && m.Slot.DurationInMinute == 60 && m.IsPreScheduled);
        }
    }
}