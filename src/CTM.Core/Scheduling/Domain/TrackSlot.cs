using System;
using System.Collections.Generic;

namespace CTM.Core.Scheduling.Domain
{
    public class TrackSlot
    {
        public TrackSlot(string title, TimeSlot timeSlot, bool isPreScheduled = false)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            TimeSlot = timeSlot ?? throw new ArgumentNullException(nameof(timeSlot));

            IsPreScheduled = isPreScheduled;
            TrackSessions = new List<TrackSession>();
        }

        public TimeSlot TimeSlot { get; }
        public string Title { get; }
        public bool IsPreScheduled { get; }

        public List<TrackSession> TrackSessions { get; }
    }
}