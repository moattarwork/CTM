using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Scheduling.Domain;
using Microsoft.Extensions.Options;

namespace CTM.Core.Scheduling
{
    public class TrackSchedulingEngine : ITrackSchedulingEngine
    {
        private readonly SchedulingOptions _schedulingOptions;
        private readonly ITrackBuilder _trackBuilder;

        public TrackSchedulingEngine(ITrackBuilder trackBuilder, IOptions<SchedulingOptions> optionAccessor)
        {
            if (optionAccessor == null) throw new ArgumentNullException(nameof(optionAccessor));
            _trackBuilder = trackBuilder ?? throw new ArgumentNullException(nameof(trackBuilder));

            _schedulingOptions = optionAccessor.Value;
        }

        public IReadOnlyList<Track> Calculate(IEnumerable<SessionDefinition> sessionDefinitions)
        {
            if (sessionDefinitions == null) throw new ArgumentNullException(nameof(sessionDefinitions));

            var tracks = _trackBuilder.Build(_schedulingOptions.ConcurrentTracks);
            var availableSlots = tracks.SelectMany(t => t.Slots).Where(s => s.IsPreScheduled == false).ToList();

            AllocateSlots(availableSlots, sessionDefinitions);

            return tracks;
        }

        private void AllocateSlots(List<TrackSlot> availableSlots, IEnumerable<SessionDefinition> sessionDefinitions)
        {
            var orderedSessions = sessionDefinitions.OrderByDescending(sd => sd.Duration).ToList();
            var index = 0;

            // Initialize the remainig capacity for each slot
            var calculatedSlots = availableSlots.Select(s => new CalculatedTrackSlot(s)).ToList();

            // Allocate
            while (index < orderedSessions.Count)
            {
                // TODO: we should check over allocation
                var allocation = calculatedSlots.Max(m => m.UnallocatedTime);

                var candidate = calculatedSlots.First(cs => cs.UnallocatedTime == allocation);
                candidate.AllocateSession(orderedSessions[index]);

                index++;
            }
        }
    }
}