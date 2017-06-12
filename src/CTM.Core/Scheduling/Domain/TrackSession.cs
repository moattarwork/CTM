using System;

namespace CTM.Core.Scheduling.Domain
{
    public class TrackSession
    {
        public TrackSession(string title, TimeSlot time)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Time = time ?? throw new ArgumentNullException(nameof(time));
        }

        public string Title { get; set; }
        public TimeSlot Time { get; set; }
    }
}