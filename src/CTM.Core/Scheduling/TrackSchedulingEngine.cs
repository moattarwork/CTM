using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Exceptions;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Scheduling.Domain;
using Microsoft.Extensions.Options;

namespace CTM.Core.Scheduling
{
    public class TrackSchedulingEngine : ITrackSchedulingEngine
    {
        private readonly ITrackSlotAllocationStrategy _slotAllocationStrategy;
        private readonly SchedulingOptions _schedulingOptions;
        private readonly ITrackBuilder _trackBuilder;

        public TrackSchedulingEngine(ITrackBuilder trackBuilder, 
            ITrackSlotAllocationStrategy slotAllocationStrategy,
            IOptions<SchedulingOptions> optionAccessor)
        {
            _slotAllocationStrategy = slotAllocationStrategy ?? throw new ArgumentNullException(nameof(slotAllocationStrategy));
            _trackBuilder = trackBuilder ?? throw new ArgumentNullException(nameof(trackBuilder));

            if (optionAccessor == null) throw new ArgumentNullException(nameof(optionAccessor));
            _schedulingOptions = optionAccessor.Value;
        }

        public IReadOnlyList<Track> Calculate(IEnumerable<SessionDefinition> sessionDefinitions)
        {
            if (sessionDefinitions == null) throw new ArgumentNullException(nameof(sessionDefinitions));

            var tracks = _trackBuilder.Build(_schedulingOptions.ConcurrentTracks);
            var availableSlots = tracks.SelectMany(t => t.Slots).Where(s => s.IsPreScheduled == false).ToList();

            var allocationResult = _slotAllocationStrategy.Allocate(availableSlots, sessionDefinitions);
            if (allocationResult == null)
                throw new AppException("Error in allocating sessions. result is null");

            if (allocationResult.UnallocatedSessions.Any())
                throw new UnallocatedSessionsException(allocationResult.UnallocatedSessions);

            return tracks;
        }
    }
}