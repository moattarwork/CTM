using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Scheduling
{
    public class TrackBuilder : ITrackBuilder
    {
        public IReadOnlyList<Track> Build(int number)
        {
            if (number <= 0) throw new ArgumentOutOfRangeException(nameof(number));

            return Enumerable.Range(1, number)
                .Select(m =>
                {
                    var defaultSlots = CreateDefaultSlots();
                    return new Track(m, defaultSlots);
                }).ToList();
        }

        private List<TrackSlot> CreateDefaultSlots()
        {
            return new List<TrackSlot>
            {
                new TrackSlot("Morning Sessions", new TimeSlot(9, 0, (int) TimeSpan.FromHours(3).TotalMinutes)),
                new TrackSlot("Lunch", new TimeSlot(12, 0, 60), true),
                new TrackSlot("Afternoon Sessions", new TimeSlot(13, 0, (int) TimeSpan.FromHours(4).TotalMinutes)),
                new TrackSlot("Networking", new TimeSlot(17, 0, 60), true)
            };
        }
    }
}