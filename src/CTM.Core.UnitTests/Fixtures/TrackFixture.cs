using System.Collections.Generic;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.UnitTests.Fixtures
{
    public class TrackFixture
    {
        public TrackFixture()
        {
            var equalSlots = new List<TrackSlot>
            {
                new TrackSlot("Slot 1", new Slot(9, 0, 180)),
                new TrackSlot("Slot 2", new Slot(13, 0, 180))
            };

            EqualSlotsTrack = new Track(1, equalSlots);

            var unequalSlots = new List<TrackSlot>
            {
                new TrackSlot("Slot 1", new Slot(9, 0, 180)),
                new TrackSlot("Slot 2", new Slot(13, 0, 240))
            };

            UnequalSlotsTrack = new Track(1, unequalSlots);

            SessionDefinitions = new List<SessionDefinition>
            {
                new SessionDefinition("Session #1", 45),
                new SessionDefinition("Session #2", 120),
                new SessionDefinition("Session #3", 5),
                new SessionDefinition("Session #4", 30),
                new SessionDefinition("Session #5", 60)
            };
        }

        public Track EqualSlotsTrack { get; }
        public Track UnequalSlotsTrack { get; }

        public List<SessionDefinition> SessionDefinitions { get; set; }
    }
}