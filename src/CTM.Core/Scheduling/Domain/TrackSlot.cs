using System;
using System.Collections.Generic;

namespace CTM.Core.Scheduling.Domain
{
    public class TrackSlot
    {
        public TrackSlot(string title, Slot slot, bool isPreScheduled = false)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Slot = slot ?? throw new ArgumentNullException(nameof(slot));

            IsPreScheduled = isPreScheduled;
            TrackSessions = new List<TrackSession>();
        }

        public Slot Slot { get; }
        public string Title { get; }
        public bool IsPreScheduled { get; }

        public List<TrackSession> TrackSessions { get; }
    }
}