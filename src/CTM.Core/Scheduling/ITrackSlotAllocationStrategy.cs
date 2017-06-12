using System.Collections.Generic;
using CTM.Core.Inputs.Parsing;
using CTM.Core.Scheduling.Domain;

namespace CTM.Core.Scheduling
{
    public interface ITrackSlotAllocationStrategy
    {
        AllocationResult Allocate(IReadOnlyList<TrackSlot> availableSlots, IEnumerable<SessionDefinition> sessionDefinitions);
    }
}