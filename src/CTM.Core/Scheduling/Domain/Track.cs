using System.Collections.Generic;

namespace CTM.Core.Scheduling.Domain
{
    public class Track
    {
        public Track(int trackNumber, List<TrackSlot> slots)
        {
            TrackNumber = trackNumber;
            Slots = slots;
        }

        public int TrackNumber { get; }

        public string DefaultTrackName => $"Track #{TrackNumber}";

        public List<TrackSlot> Slots { get; }
    }
}