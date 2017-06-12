using System;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Scheduling.Extensions
{
    public static class TrackExtensions
    {
        public static Slot GetNextSlot(this TrackSlot trackSlot, int duration)
        {
            if (trackSlot == null) throw new ArgumentNullException(nameof(trackSlot));
            if (duration <= 0) throw new ArgumentOutOfRangeException(nameof(duration));

            var datetime = DateTime.Today
                .AddHours(trackSlot.Slot.Hour)
                .AddMinutes(trackSlot.Slot.Minute);

            return new Slot(datetime.Hour, datetime.Minute, duration);
        }

        public static Slot GetNextSlot(this TrackSession trackSession, int duration)
        {
            if (trackSession == null) throw new ArgumentNullException(nameof(trackSession));
            if (duration <= 0) throw new ArgumentOutOfRangeException(nameof(duration));

            var datetime = DateTime.Today
                .AddHours(trackSession.Time.Hour)
                .AddMinutes(trackSession.Time.Minute + trackSession.Time.DurationInMinute);

            return new Slot(datetime.Hour, datetime.Minute, duration);
        }
    }
}