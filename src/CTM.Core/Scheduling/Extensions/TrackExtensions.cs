using System;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Scheduling.Extensions
{
    public static class TrackExtensions
    {
        public static TimeSlot GetNextSlot(this TrackSlot trackSlot, int duration)
        {
            if (trackSlot == null) throw new ArgumentNullException(nameof(trackSlot));
            if (duration <= 0) throw new ArgumentOutOfRangeException(nameof(duration));

            var datetime = DateTime.Today
                .AddHours(trackSlot.TimeSlot.Hour)
                .AddMinutes(trackSlot.TimeSlot.Minute);

            return new TimeSlot(datetime.Hour, datetime.Minute, duration);
        }

        public static TimeSlot GetNextSlot(this TrackSession trackSession, int duration)
        {
            if (trackSession == null) throw new ArgumentNullException(nameof(trackSession));
            if (duration <= 0) throw new ArgumentOutOfRangeException(nameof(duration));

            var datetime = DateTime.Today
                .AddHours(trackSession.Time.Hour)
                .AddMinutes(trackSession.Time.Minute + trackSession.Time.DurationInMinute);

            return new TimeSlot(datetime.Hour, datetime.Minute, duration);
        }
    }
}