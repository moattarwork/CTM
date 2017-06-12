using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Scheduling
{
    public class RoundRabinSlotAllocationStrategy : ITrackSlotAllocationStrategy
    {
        public AllocationResult Allocate(IReadOnlyList<TrackSlot> availableSlots, IEnumerable<SessionDefinition> sessionDefinitions)
        {
            if (availableSlots == null) throw new ArgumentNullException(nameof(availableSlots));
            if (sessionDefinitions == null) throw new ArgumentNullException(nameof(sessionDefinitions));

            var unallocatedSessions = new List<SessionDefinition>();

            var orderedSessions = sessionDefinitions.OrderByDescending(sd => sd.Duration).ToList();
            var index = 0;

            // Initialize the remainig capacity for each timeSlot
            var calculatedSlots = availableSlots.Select(s => new CalculatedTrackSlot(s)).ToList();

            // Allocate
            while (index < orderedSessions.Count)
            {
                var session = orderedSessions[index];
                index++;

                var allocation = calculatedSlots.Max(m => m.UnallocatedTime);
                if (allocation < session.Duration)
                {
                    unallocatedSessions.Add(session);
                    continue;
                }

                var candidate = calculatedSlots.First(cs => cs.UnallocatedTime == allocation);
                candidate.AllocateSession(session);
            }

            return new AllocationResult(availableSlots, unallocatedSessions);
        }
    }
}