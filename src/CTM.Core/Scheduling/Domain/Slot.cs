using System;

namespace CTM.Core.Scheduling.Domain
{
    public class Slot
    {
        public Slot(int hour, int minute, int durationInMinute)
        {
            if (hour <= 0 || hour > 23) throw new ArgumentOutOfRangeException(nameof(hour));
            if (minute < 0 || minute > 59) throw new ArgumentOutOfRangeException(nameof(minute));
            if (durationInMinute <= 0) throw new ArgumentOutOfRangeException(nameof(durationInMinute));

            Hour = hour;
            Minute = minute;
            DurationInMinute = durationInMinute;
        }

        public int Hour { get; }
        public int Minute { get; }
        public int DurationInMinute { get; }

        public string ToTimeString()
        {
            var date = DateTime.Today.AddHours(Hour).AddMinutes(Minute);

            return $"{date:hh:mmtt}";
        }
    }
}