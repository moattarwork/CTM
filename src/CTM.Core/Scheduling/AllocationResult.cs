using System;
using System.Collections.Generic;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Scheduling
{
    public class AllocationResult
    {
        public AllocationResult(IReadOnlyList<TrackSlot> slots, IReadOnlyList<SessionDefinition> unallocatedSessions)
        {
            Slots = slots ?? throw new ArgumentNullException(nameof(slots));
            UnallocatedSessions = unallocatedSessions ?? throw new ArgumentNullException(nameof(unallocatedSessions));
        }

        public IReadOnlyList<TrackSlot> Slots { get; }
        public IReadOnlyList<SessionDefinition> UnallocatedSessions { get; }
    }
}